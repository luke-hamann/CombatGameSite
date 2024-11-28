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
        { //View Teams here. Add Edit, and Delete functions would be listed in this view
            return View();
        }

        [HttpGet]
        public IActionResult Add(Team team) 
        { //View a Blank Character Form. Select team that you add to in get data or in form?
            return View();
        }

        [HttpPost]
        public IActionResult Add(Combatant character,  Team team)
        { //Edit an existing character

            return View();
        }

        [HttpPost]
        public IActionResult Delete(Combatant character)
        { //Remove a Character from a slot in a team.
            return View();
        }


    }
}
