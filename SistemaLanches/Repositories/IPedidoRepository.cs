using SistemaLanches.Models;

namespace SistemaLanches.Repositories
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}