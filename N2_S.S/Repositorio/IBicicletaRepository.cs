using N2_S.S.Model;

namespace N2_S.S.Repositorio
{
    public interface IBicicletaRepository
    {
        Task<IEnumerable<BicicletasModel>> BuscarTodasBicletasAsync();
        Task<BicicletasModel> BuscarBicicletaCorMarcaAsync(string marca, string cor);

        Task<bool> AdicionarAsync (BicicletasModel model);
        Task<bool> AtualizarAsync (BicicletasModel model, int cod_bicicleta);
        Task<bool> DeletarAsync(int icod_bicicletad);
        
    }
}
