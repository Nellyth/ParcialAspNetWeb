using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialAspNetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace ParcialAspNetWeb.Controllers
{
	public class PqrsController : Controller
	{
		// GET: PqrsController
		public async Task<ActionResult> Index()
		{
			var httpClient = new HttpClient();
			var pqrJson = await httpClient.GetStringAsync("https://localhost:44379/api/pqr");
			List<PQR> pqrsList = JsonConvert.DeserializeObject<List<PQR>>(pqrJson);

			return View(pqrsList);
		}

		// GET: PqrsController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var httpClient = new HttpClient();
			var pqrJson = await httpClient.GetStringAsync("https://localhost:44379/api/pqr/" + id);
			PQR pqrs = JsonConvert.DeserializeObject<PQR>(pqrJson);
			return View(pqrs);
		}

		// GET: PqrsController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: PqrsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PqrsController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var httpClient = new HttpClient();
			var pqrJson = await httpClient.GetStringAsync("https://localhost:44379/api/pqr/" + id);
			PQR pqrs = JsonConvert.DeserializeObject<PQR>(pqrJson);
			return View(pqrs);
		}

		// POST: PqrsController/Edit/5
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, PQR pqr, User user)
		{
			try
			{
				pqr.idPqr = id;
				pqr.usuario = user;
				var httpClient = new HttpClient();
				var response = await httpClient.PutAsync("https://localhost:44379/api/pqr/" + id, pqr, new JsonMediaTypeFormatter());
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction(nameof(Index));
				} else
				{
					//HttpResponseMessage responseJson = response;
					//Console.WriteLine(responseJson.Content.ReadAsStringAsync().Result);
					return RedirectToAction(nameof(Index));
				}
			}
			catch
			{
				return View();
			}
		}

		// GET: PqrsController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var httpClient = new HttpClient();
			var response = await httpClient.DeleteAsync("https://localhost:44379/api/pqr/" + id);
			return RedirectToAction(nameof(Index));
		}

		// POST: PqrsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
