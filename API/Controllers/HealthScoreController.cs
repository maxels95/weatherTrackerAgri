using Microsoft.AspNetCore.Mvc;
using AgriWeatherTracker.Models;
using AgriWeatherTracker.Data;
using AutoMapper;
using AgriWeatherTracker.Service;

[Route("api/[controller]")]
[ApiController]
public class HealthScoreController : ControllerBase
{
    private readonly IHealthScoreRepository _healthScoreRepository;
    private readonly ICropRepository _cropRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IConditionThresholdRepository _conditionThresholdRepository;
    private readonly HealthEvaluatorService _healthEvaluatorService;
    private readonly IMapper _mapper;

    public HealthScoreController(IHealthScoreRepository healthScoreRepository,
        ICropRepository cropRepository, ILocationRepository locationRepository, 
        IMapper mapper,
        IConditionThresholdRepository conditionThresholdRepository, 
        HealthEvaluatorService healthEvaluatorService)
    {
        _healthScoreRepository = healthScoreRepository ?? throw new ArgumentNullException(nameof(healthScoreRepository));
        _cropRepository = cropRepository ?? throw new ArgumentNullException(nameof(cropRepository));
        _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
        _conditionThresholdRepository = conditionThresholdRepository ?? throw new ArgumentNullException(nameof(conditionThresholdRepository));
        _healthEvaluatorService = healthEvaluatorService ?? throw new ArgumentNullException(nameof(healthEvaluatorService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HealthScore>>> GetHealthScore()
    {
        try
        {
            var healthScores = await _healthScoreRepository.GetAllHealthScoresAsync();
            if (healthScores == null || !healthScores.Any())
            {
                return NotFound("No health scores found.");
            }
            return Ok(healthScores);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/HealthScore/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<HealthScoreDto>> GetHealthScore(int id)
    {
        try
        {
            var healthScore = await _healthScoreRepository.GetHealthScoreByIdAsync(id);
            if (healthScore == null)
            {
                return NotFound($"HealthScore with ID {id} not found.");
            }
            return Ok(_mapper.Map<HealthScoreDto>(healthScore));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("byCrop/{locationId}")]
    public async Task<ActionResult<HealthScoreDto>> GetHealthScoresByLocation(int locationId)
    {
        try
        {
            var healthScore = await _healthScoreRepository.GetHealthScoreByLocationIdAsync(locationId);
            if (healthScore == null)
            {
                return NotFound($"No health scores found for location with ID {locationId}.");
            }
            return Ok(_mapper.Map<HealthScoreDto>(healthScore));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/HealthScore
    [HttpPost]
    public async Task<ActionResult<HealthScoreDto>> PostHealthScore(HealthScoreDto healthScoreDto)
    {
        try
        {
            var crop = await _cropRepository.GetCropByIdAsync(healthScoreDto.CropId);
            if (crop == null)
            {
                return NotFound($"Crop with ID {healthScoreDto.CropId} not found.");
            }

            var healthScore = _mapper.Map<HealthScore>(healthScoreDto);
            _cropRepository.SetEntityStateUnchanged(crop);
            healthScore.Crop = crop;

            await _healthScoreRepository.CreateHealthScoreAsync(healthScore);
            return CreatedAtAction(nameof(GetHealthScore), new { id = healthScore.Id }, _mapper.Map<HealthScoreDto>(healthScore));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/HealthScore/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHealthScore(int id, double score)
    {
        try
        {
            var healthScore = await _healthScoreRepository.GetHealthScoreByIdAsync(id);
            if (healthScore == null)
            {
                return NotFound($"HealthScore with ID {id} not found.");
            }

            healthScore.Score = score;
            healthScore.Date = DateTime.UtcNow;
            await _healthScoreRepository.UpdateHealthScoreAsync(healthScore);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }   
    }
    // PUT: api/HealthScore/{id}
    // [HttpPut("{id}")]
    // public async Task<ActionResult<HealthScoreDto>> UpdateHealthScore(int id, WeatherDTO weatherDTO)
    // {
    //     var healthScore = await _healthScoreRepository.GetHealthScoreByIdAsync(id);
    //     if (healthScore == null)
    //     {
    //         return NotFound($"HealthScore with ID {id} not found.");
    //     }

    //     // Assume the ConditionThreshold is retrieved based on some logic or another service
    //     var threshold = _conditionThresholdRepository.; // This needs to be properly fetched or passed in

    //     // Check if the new weather data is newer than the last update
    //     if (weatherDTO.Date > healthScore.Date)
    //     {
    //         // Evaluate the temperature impact starting from the current score
    //         double updatedScore = _healthEvaluatorService.EvaluateTemperatureImpact(weatherDTO, threshold, 
    //             _mapper.Map<HealthScoreDto>(healthScore));
            
    //         // Update the health score and date with the new values
    //         healthScore.Score = updatedScore;
    //         healthScore.Date = weatherDTO.Date;

    //         await _healthScoreRepository.UpdateHealthScoreAsync(healthScore);
    //         return Ok(_mapper.Map<HealthScoreDto>(healthScore)); // Return updated score
    //     }

    //     return BadRequest("The provided weather data is not newer than the existing score data.");
    // }
}
