﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HockeySignups.Data;
using HockeySignups.Web.Models;

namespace HockeySignups.Web.Controllers
{
    public class AdminController : Controller
    {
        private string _connectionString =
            @"Data Source=.\sqlexpress;Initial Catalog=HockeySignup;Integrated Security=True";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(DateTime date, int maxPeople)
        {
            var db = new HockeySignupsDb(_connectionString);
            Event e = new Event { Date = date, MaxPeople = maxPeople };
            db.AddEvent(e);
            TempData["Message"] = "Event Successfully created, Id: " + e.Id;
            IEnumerable<NotificationSignup> signups = db.GetNotificationSignups();
            foreach (NotificationSignup signup in signups)
            {
                //send email using signup.Email
            }
            return RedirectToAction("Index", "Hockey");
        }

        public ActionResult HistoryWithGroupBy()
        {
            var db = new HockeySignupsDb(_connectionString);
            IEnumerable<EventWithPeople> events = db.GetEventsWithCount();
            return View(events);
        }

        public ActionResult History()
        {
            var db = new HockeySignupsDb(_connectionString);
            IEnumerable<Event> events = db.GetEvents();
            //longer approach
            HistoryViewModel vm = new HistoryViewModel();
            //List<EventWithCount> eventsWithCounts = new List<EventWithCount>();
            //foreach (Event e in events)
            //{
            //    EventWithCount eventWithCount = new EventWithCount();
            //    eventWithCount.Event = e;
            //    eventWithCount.SignupCount = db.GetSignupCountForEvent(e.Id);
            //    eventsWithCounts.Add(eventWithCount);
            //}

            //vm.Events = eventsWithCounts;

            //shorter approach
            vm.Events = events.Select(e => new EventWithCount
            {
                Event = e,
                SignupCount = db.GetSignupCountForEvent(e.Id)
            });


            return View(vm);
        }

        public ActionResult EventDetails(int id)
        {
            var db = new HockeySignupsDb(_connectionString);
            Event e = db.GetEventById(id);
            IEnumerable<EventSignup> signups = db.GetEventSignups(id);
            var vm = new EventDetailsViewModel
            {
                Event = e,
                Signups = signups
            };
            return View(vm);
        }

    }
}
