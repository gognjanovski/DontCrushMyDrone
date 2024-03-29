﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DroneCrush.DataContext;
using DroneCrush.Models;

namespace DroneCrush.Controllers.WebApi
{
    public class DronesController : ApiController
    {
        private DroneDb db = new DroneDb();

        // GET: api/Drones
        public IQueryable<Drone> GetDrone()
        {
            return db.Drone;
        }

        // GET: api/Drones/5
        [ResponseType(typeof(Drone))]
        public IHttpActionResult GetDrone(int id)
        {
            Drone drone = db.Drone.Find(id);
            if (drone == null)
            {
                return NotFound();
            }

            return Ok(drone);
        }

        // PUT: api/Drones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDrone(int id, Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drone.ID)
            {
                return BadRequest();
            }

            db.Entry(drone).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DroneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Drones
        [ResponseType(typeof(Drone))]
        public IHttpActionResult PostDrone(Drone drone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drone.Add(drone);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = drone.ID }, drone);
        }

        // DELETE: api/Drones/5
        [ResponseType(typeof(Drone))]
        public IHttpActionResult DeleteDrone(int id)
        {
            Drone drone = db.Drone.Find(id);
            if (drone == null)
            {
                return NotFound();
            }

            db.Drone.Remove(drone);
            db.SaveChanges();

            return Ok(drone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DroneExists(int id)
        {
            return db.Drone.Count(e => e.ID == id) > 0;
        }
    }
}