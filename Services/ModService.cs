using shortenyour.link.Data;
namespace shortenyour.link.Services
{
    public class ModService
    {
        private readonly ModsContext _modsContext;

        public ModService(ModsContext modsContext)
        {
            _modsContext = modsContext;
        }
        public bool MaintenanceIsEnabled()
        {
            var entity = _modsContext.mods.FirstOrDefault();
            return entity != null && entity.Maintenance == "yes";
        }
    }
}