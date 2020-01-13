using Microsoft.Practices.Unity;
using StockChessCS.Interfaces;
using StockChessCS.Services;
using StockChessCS.ViewModels;

namespace StockChessCS.Helpers
{
    public class ViewModelLocator
    {

        private UnityContainer _container;
        public ViewModelLocator()
        {
            _container = new UnityContainer();
            _container.RegisterType<IEngineService, StockfishService>();
        }

        public ChessViewModel ChessVM
        {
            get { return _container.Resolve<ChessViewModel>(); }
        }
    }
}
