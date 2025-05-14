using System.Net.Mime;
using AlquilaFacilPlatform.Profiles.Domain.Model.Queries;
using AlquilaFacilPlatform.Profiles.Domain.Services;
using AlquilaFacilPlatform.Profiles.Interfaces.REST.Resources;
using AlquilaFacilPlatform.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Profiles.Interfaces.REST.Transform;

namespace AlquilaFacilPlatform.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ProfilesController(
    IProfileCommandService profileCommandService, 
    IProfileQueryService profileQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileResource createProfileResource)
    {
        try {
            var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(createProfileResource);
            var profile = await profileCommandService.Handle(createProfileCommand);
            if (profile is null) return BadRequest();
            var resource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
            return CreatedAtAction(nameof(GetProfileById), new {profileId = resource.Id}, resource);
        }catch(Exception e) {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProfiles()
    {
        try {
            var getAllProfilesQuery = new GetAllProfilesQuery();
            var profiles = await profileQueryService.Handle(getAllProfilesQuery);
            var resources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }catch(Exception e) {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpGet("{profileId}")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        try {
            var profile = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
            if (profile == null) return NotFound();
            var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
            return Ok(profileResource);
        }catch(Exception e) {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetProfileByUserId(int userId)
    {
        try {
            var profile = await profileQueryService.Handle(new GetProfileByUserIdQuery(userId));
            if (profile == null) return NotFound();
            var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
            return Ok(profileResource);
        }catch(Exception e) {
            return BadRequest(new {message = e.Message});
        }
    }
    
    [HttpGet("is-user-subscribed/{userId}")]
    public async Task<IActionResult> IsUserSubscribed(int userId)
    {
        try {
            var query = new IsUserSubscribeQuery(userId);
            var user = await profileQueryService.Handle(query);
            return Ok(user);
        }catch(Exception e) {
            return BadRequest(new {message = e.Message});
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFarm(int id,[FromBody] UpdateProfileResource updateProfileResource)
    {
        try {
            var updateProfileCommand = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(updateProfileResource,id);
            var result = await profileCommandService.Handle(updateProfileCommand);
            return Ok(ProfileResourceFromEntityAssembler.ToResourceFromEntity(result));
        }catch(Exception e) {
            return BadRequest(new {message = e.Message});
        }
    }
}