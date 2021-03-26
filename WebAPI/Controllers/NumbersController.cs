using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneBookLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for phonebook entries
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NumbersController : ControllerBase
    {


        /// <summary>
        /// Get phone library
        /// </summary>
        /// <param name="sort">Optional sort parameter can be "FirstName" or "LastName"</param>
        /// <returns>Phone library with or without sorting depending on parameter.</returns>
        [HttpGet]
        public PhoneBookLibrary.PhoneLibrary Get(string sort)
        {            
            var library = new PhoneBookLibrary.PhoneLibrary(sort);
            return library;
        }

        /// <summary>
        /// Add new entry to the phonebook
        /// </summary>
        /// <param name="entry">Takes entry class</param>
        /// <returns></returns>
        [HttpPost]
        public string Create(Entry entry)
        {
            if (ModelState.IsValid)
            {
                PhoneLibrary library = new PhoneLibrary();
                library.AddEntry(entry);
                return "Entry created successfuly.";
            }
            else
            {
                
                return "Entry did not pass validation.";                
            }
            
        }


        /// <summary>
        /// Deletes entry with provided ID
        /// </summary>
        /// <param name="id">ID of entry</param>
        /// <returns></returns>
        [HttpGet("~/Delete")]
        public string Delete(string id)
        {
            PhoneLibrary library = new PhoneLibrary();
            string result = library.DeleteEntry(id);
            return result;
        }


        /// <summary>
        /// Edits an entry.
        /// </summary>
        /// <param name="entry">Provide entry with ID, and parameters.</param>
        /// <returns>If edit was successful or not.</returns>
        [HttpPost("~/Edit")]
        public string Edit(Entry entry)
        {
            PhoneLibrary library = new PhoneLibrary();
            var result = library.Edit(entry);
            return result;
        }



    }
}
