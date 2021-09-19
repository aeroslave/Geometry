using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using GeometryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeometryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Контроллер взаимодействия с геометрией.
    /// </summary>
    public class GeometryController : ControllerBase
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly GeometryContext _context;

        /// <summary>
        /// Контроллер взаимодействия с геометрией.
        /// </summary>
        public GeometryController(GeometryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все значения в БД.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Figure>> Get()
        {
            return await _context.Figures.ToListAsync();
        }

        /// <summary>
        /// Получить круги, у которых родитель квадрат.
        /// </summary>
        /// <param name="date">Дата выборки.</param>
        [Route("getCircles")]
        [HttpGet]
        public async Task<List<Figure>> GetCircles(string date)
        {
            var circles = _context.Figures.FromSqlRaw($"SELECT * FROM \"GetCircles\"('{date}')");
            return await circles.ToListAsync();
        }

        /// <summary>
        /// Получить упорядоченные от корня фигуры.
        /// </summary>
        /// <param name="date">Дата выборки.</param>
        [Route("getOrderedFigures")]
        [HttpGet]
        public async Task<List<Figure>> GetOrderedFigures(string date)
        {
            var orderedFigures = _context.Figures.FromSqlRaw($"SELECT * FROM \"GetOrderedFigures\"('{date}')");
            return await orderedFigures.ToListAsync();
        }

        /// <summary>
        /// Добавить данные о фигуре.
        /// </summary>
        [HttpPost]
        public async Task<long> Post(Figure figure)
        {
            var dbFigure = await _context.Figures.FirstOrDefaultAsync(it => it.DateAndTime == figure.DateAndTime && it.Id == figure.Id);
            if (dbFigure != null)
            {
                throw new ArgumentException("Такие данные уже добавлены");
            }

            _context.Figures.Add(figure);
            await _context.SaveChangesAsync();

            return figure.Id;
        }
        
        /// <summary>
        /// Изменить данные о фигуре
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Put(Figure figure)
        {
            var dbFigure = await _context.Figures.FirstOrDefaultAsync(it => it.DateAndTime == figure.DateAndTime && it.Id == figure.Id);
            if (dbFigure == null)
            {
                return NotFound();
            }

            dbFigure.Area = figure.Area;
            dbFigure.ParentId = figure.ParentId;
            dbFigure.Type = figure.Type;

            return Ok();
        }

        /// <summary>
        /// Удалить данные о фигуре.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, string date)
        {
            var dbFigure = await _context.Figures.FirstOrDefaultAsync(it => it.DateAndTime == DateTime.Parse(date) && it.Id == id);
            if (dbFigure == null)
            {
                return NotFound();
            }

            _context.Figures.Remove(dbFigure);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
