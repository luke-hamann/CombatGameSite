using System.Diagnostics;
using CombatGameSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CombatGameSite.Controllers
{
    [Area("Default")]
    public class CharacterController
    {
        
        [HttpGet]
        public IActionResult View()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int character)
        { //Change int to character class once built
            return View();
        }

        [HttpGet]
        public IActionResult Delete()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int character)
        { //Change int to character class once built
            return View();
        }

    }
}
