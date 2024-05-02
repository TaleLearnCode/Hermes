using Microsoft.EntityFrameworkCore;

namespace Hermes.Models;

public partial class HermesContext
{

	public readonly string connectionString;

	public HermesContext(string connectionString) => this.connectionString = connectionString;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer(connectionString);

}