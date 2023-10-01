using Flunt.Notifications;
using BaseResponse = JwtStore.Core.Contexts.SharedContext.UseCases.Response;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Response : BaseResponse
{
  public ResponseData? Data { get; set; }

  protected Response() { }

  public Response(string message, int status, IEnumerable<Notification>? notifications = null)
  {
    Message = message;
    Status = status;
    Notifications = notifications;
  }

  public Response(string message, ResponseData data)
  {
    Message = message;
    Status = 201;
    Notifications = null;
    Data = data;
  }
}

public record ResponseData(Guid Id, string Name, string Email);
