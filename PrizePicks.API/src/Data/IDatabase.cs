using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public interface IDatabase
{
    public Task<IEnumerable<ICage>> CagesAsync();
    public void Remove(ICage instance);
    public void Update(ICage instance);

    public Task<IEnumerable<IDinosaur>> DinosaursAsync();
    public void Remove(IDinosaur instance);
    public void Update(IDinosaur instance);
}
