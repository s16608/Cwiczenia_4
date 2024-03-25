namespace LegacyApp.Interfaces;

public interface IClientRepository
{
    public Client GetById(int clientId);
}