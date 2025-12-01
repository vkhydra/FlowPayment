using Microsoft.AspNetCore.Mvc;
using FlowPayment.Application.DTOs;
using FlowPayment.Application.Interfaces;

namespace FlowPayment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletsController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto dto)
        {
            try
            {
                var result = await _walletService.CreateWalletAsync(dto);
                return CreatedAtAction(nameof(CreateWallet), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> DepositWallet([FromBody] DepositDto dto)
        {
            try
            {
                var result = await _walletService.DepositAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferWallet([FromBody] TransferDto dto)
        {
            try
            {
                var result =  await _walletService.TransferAsync(dto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}