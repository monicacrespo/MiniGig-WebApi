namespace MiniGigWebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using AutoMapper;
    using MiniGigWebApi.Domain;
    using MiniGigWebApi.Services;

    [RoutePrefix("api/gigs")]
    public class GigsController : ApiController
    {
        private readonly IGigService gigService;
        private readonly IMapper mapper;

        public GigsController(IGigService gigService, IMapper mapper)
        {
            this.gigService = gigService;
            this.mapper = mapper;
        }

        [Route]
        public async Task<IHttpActionResult> Get(int page = 0, int pageSize = 5)
        {
            try
            {
                var result = await this.gigService.GetGigs(page, pageSize);

                var mappedResult = this.mapper.Map<IEnumerable<GigModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{gigId}", Name = "GetGig")]
        public async Task<IHttpActionResult> Get(int gigId)
        {
            try
            {
                var result = await this.gigService.GetGigDetails(gigId);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(this.mapper.Map<GigModel>(result));
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route]
        public async Task<IHttpActionResult> Post(Gig gig)
        {
            try
            {
                if (await this.gigService.GetGig(gig.Id) != null)
                {
                    ModelState.AddModelError("GigId", "Gig in use");
                }

                if (ModelState.IsValid)
                {
                    await this.gigService.InsertGig(gig);
                    return CreatedAtRoute("GetGig", new { gigId = gig.Id }, gig);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }

        [Route("{gigId}")]
        public async Task<IHttpActionResult> Put(int gigId, Gig gig)
        {
            try
            {
                if (await this.gigService.GetGig(gigId) == null)
                {
                    return NotFound();
                }

                await gigService.UpdateGig(gig);

                return Ok(gig);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{gigId}")]
        public async Task<IHttpActionResult> Delete(int gigId)
        {
            try
            {
                var gig = await this.gigService.GetGig(gigId);
                if (gig == null)
                {
                    return NotFound();
                }

                await this.gigService.DeleteGig(gig);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}