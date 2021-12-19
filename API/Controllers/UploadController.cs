using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly ILogger<UploadController> _logger;

    public UploadController(ILogger<UploadController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public IActionResult Post(IFormFile image)
    {
        try
        {
            //validação simples, caso não tenha sido enviado nenhuma imagem para upload nós estamos retornando null
            if (image == null) return null;

            //Salvando a imagem no formato enviado pelo usuário
            using (var stream = new FileStream(Path.Combine("Imagens", image.FileName), FileMode.Create))
            {
                image.CopyTo(stream);
            }            

            return Ok(new
            {
                mensagem = "Imagem salva com sucesso!",
                urlImagem = $"http://localhost:5055/img/{image.FileName}"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Erro no upload: " + ex.Message);
        }

    }

}
