using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AgriWeatherTracker.Models;
using AgriWeatherTracker.Repository;

namespace AgriWeatherTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalGeneratedController : ControllerBase
    {
        private readonly ISignalGeneratedRepository _signalGeneratedRepository;

        public SignalGeneratedController(ISignalGeneratedRepository signalGeneratedRepository)
        {
            _signalGeneratedRepository = signalGeneratedRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SignalGenerated>>> GetSignals()
        {
            var signals = await _signalGeneratedRepository.GetAllSignalsAsync();
            return Ok(signals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SignalGenerated>> GetSignal(int id)
        {
            var signal = await _signalGeneratedRepository.GetSignalByIdAsync(id);
            if (signal == null)
            {
                return NotFound();
            }

            return Ok(signal);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSignal(SignalGenerated signalGenerated)
        {
            await _signalGeneratedRepository.AddSignalAsync(signalGenerated);
            return CreatedAtAction(nameof(GetSignal), new { id = signalGenerated.Id }, signalGenerated);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSignal(int id, SignalGenerated signalGenerated)
        {
            if (id != signalGenerated.Id)
            {
                return BadRequest();
            }

            await _signalGeneratedRepository.UpdateSignalAsync(signalGenerated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSignal(int id)
        {
            await _signalGeneratedRepository.DeleteSignalAsync(id);
            return NoContent();
        }
    }
}
