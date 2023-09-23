﻿using mf_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace mf_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            if(model.AnoFabricacao <= 0 )
            {
                return BadRequest(new {message = "Ano de fabricação é obrigatório e deve ser informado"});
            }
            if (model.AnoModelo <= 0)
            {
                return BadRequest(new { message = "Ano modelo é obrigatório e deve ser informado" });
            }
            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) NotFound();

            return Ok(model);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if (id != model.Id) return BadRequest();
            var modeloDB = await _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDB == null) return NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Veiculos.FindAsync(id);

            if (model == null) NotFound();

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
