using ClientRepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data.Entity; 
using System.Linq;
//using System.Web.Mvc;
using ClientRepository.Models;
using ClientRepository.Data;


public class ClientsController : Controller
{
    private readonly ClientContext _context;

    public ClientsController(ClientContext clientContext)
    {
        _context = clientContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var clients = _context.Clients.ToList();
        return View(clients);
    }

    [HttpPost]
    public IActionResult Create(Client client)
    {
        if (ModelState.IsValid)
        {
            _context.Add(client);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(client);
    }

    

    
    // XML Import
    [HttpPost]
    public IActionResult ImportXml(IFormFile file)
    {
        if(file == null || file.Length==0)
        {
            ModelState.AddModelError("File error", "Please upload valid XML file!");
            return View(Index);
        }

        try
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                var xml = stream.ReadToEnd();
                XDocument xdocument = XDocument.Parse(xml);

                var clients = xdocument.Descendants("Client")
                    .Select(c => new Client
                    {
                        ClientRefNum = int.Parse(c.Attribute("ID")?.Value), 
                        FullName = c.Element("Name")?.Value,
                        BirthDate = DateTime.Parse(c.Element("BirthDate")?.Value),
                        Addresses = c.Element("Addresses")?.Elements("Address")
                            .Select(a => new Address
                            {
                                AddressType = (int)a.Attribute("Type"), 
                                FullAddress = a.Value                               
                            }).ToList()
                    }).ToList();

                _context.Clients.AddRange(clients);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ModelState.AddModelError("File", $"Error importing XML: {e.Message}");
            return View();
        }
    }

    /*
    // JSON Export
    public IActionResult ExportJson()
    {
        var clients = _context.Clients.OrderBy(c => c.Name).ThenBy(c => c.BirthDate).ToList();
        return Json(clients);
    }
    */
}
