namespace web_services_ielectric.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}