﻿using RRHH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RRHH.Controllers
{
    public class UsersController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            UsersManager uManager = new UsersManager();
            string message = "";
            if (ModelState.IsValid)
            {
                if (uManager.CheckUser(user) > 0)
                {
                    message = "Correcto";
                    user = uManager.Consultar(user.Email);
                    Session["UsuarioLogueado"] = user;
                }
                else
                {
                    message = "Usuario o Contraseña incorrecta";

                }
            }
            else
            {
                message = "Todos los campos son requeridos";
            }

            if (Request.IsAjaxRequest())
            {
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["UsuarioLogueado"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login2()
        {
            return View();
        }

        public ActionResult DoLogin2(string email, string password)
        {
            UsersManager uManager = new UsersManager();
            Users user = uManager.Validar(email, password);

            if (user != null)
            {
                //Usuario y contraseña CORRECTO
                Session["UsuarioLogueado"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //EL USUARIO NO EXISTE O ESTA MAL LA CONTRASEÑA
                TempData["Error"] = "El usuario no existe o está mal la contraseña";
                return RedirectToAction("Login2", "Users");
            }
        }

        public ActionResult Registro()
        {
            ProvincesManager pManager = new ProvincesManager();
            ViewBag.Provinces = pManager.ConsultarTodas();
            return View();
        }
        [HttpPost]
        public ActionResult GetCiudades(int id)
        {
            List<Citys> ciudades = new List<Citys>();
            ciudades = new CitysManager().GetCiudadesPorProvincia(id);
            return Json(new SelectList(ciudades, "ID", "Name"));
        }
        [HttpPost]
        public ActionResult GuardarNuevoUsuario(string first_name, string last_name, string password, string email, string phone, string address, int province_id, int city_id, string genre)
        {
            Users newUser = new Users();
            newUser.FirstName = first_name;
            newUser.LastName = last_name;
            newUser.Password = password;
            newUser.Email = email;
            newUser.Phone = phone;
            newUser.Address = address;
            newUser.Genre = genre;
            ProvincesManager pManager = new ProvincesManager();
            newUser.Province = pManager.Consultar(province_id);
            CitysManager cManager = new CitysManager();
            newUser.City = cManager.Consultar(city_id);

            UsersManager uManager = new UsersManager();
            uManager.Insertar(newUser);

            return RedirectToAction("Index", "Home");
        }
    }
}