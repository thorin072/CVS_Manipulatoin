using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinipulVS
{
    public class Controller
    {
        private IFormView _formView;
        private readonly IImageProccesingServise _imgProccesServise;
        private readonly IStateStorageService _stateStorageService;
        private readonly IClusterServise _clusterServise;
        private readonly IMessageServise _messageServise;
        private readonly IGraphServise _graphServise;
        private readonly IInterpritateServise _interpritateServise;
        private readonly IExcelServise _excelServise;

        public Controller(IFormView formView)
        {
            _formView = formView;
            _imgProccesServise = new ImageProccesingServise();
            _stateStorageService = new StateStorageService();
            _clusterServise = new ClusterServise();
            _messageServise = new MessageServise();
            _graphServise = new GraphServise();
            _interpritateServise = new InterpritateServise();
            _excelServise = new ExcelServise();
        }
        //public Controller()
        //{
        //    _imgProccesServise = new ImageProccesingServise();
        //    _stateStorageService = new StateStorageService();
        //    _clusterServise = new ClusterServise();
        //    _messageServise = new MessageServise();
        //    _graphServise = new GraphServise();
        //    _interpritateServise = new InterpritateServise();
        //    _excelServise = new ExcelServise();
        //}
        //public Main ViewForm()
        //{
        //    _formView = new Main(this);
        //    return new Main(this);
        //}

        /// <summary>
        /// Загрузить изображение в память
        /// </summary>
        /// <param name="path">Путь изображения</param>
        public void InputImg(string path)
        {
            try
            {
                var img = _imgProccesServise.GetInputImg(path); // получение Bitmap
                _stateStorageService.SaveImg(img); // сохранение в памяти
                _formView.ChangeStatus("Изображение готово к обработке. Для поиска ключ-точек примините детектор Харриса");
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Попытка ввести в систему файл иного формата, чем: png, gif, jpeg, jpg.");
            }
        }

        /// <summary>
        ///  Получить изображение с ключ-точками
        /// </summary>
        /// <param name="sigma"></param>
        /// <param name="K"></param>
        /// <param name="th"></param>
        /// <param name="h">Высота</param>
        /// <param name="w">Ширина</param>
        public void GetImageWithKeys(double sigma, float K, float th, int h, int w)
        {
            try
            {
                var input = _stateStorageService.GetImg();// получить копию исходного изображения
                var imgWithKey = _imgProccesServise.KeyPoint(sigma, K, th, input, h, w);
                _formView.ViewImgInput(imgWithKey);
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }
        }

        /// <summary>
        /// Получить массив с ключ-точками
        /// </summary>
        /// <param name="sigma"></param>
        /// <param name="K"></param>
        /// <param name="th"></param>
        /// <param name="h"></param>
        /// <param name="w"></param>
        public void GetKeyToArr(double sigma, float K, float th, int h, int w)
        {
            try
            {
                var input = _stateStorageService.GetImg();// получить копию исходного изображения
                var cloneInput = _imgProccesServise.Clone(input.Height, input.Width, input);// клонирование исходного и разворот
                var KeyArrAccord = _imgProccesServise.KeyPoint(sigma, K, th, cloneInput);
                var KeysArr = _clusterServise.GetPoint(KeyArrAccord);
                _stateStorageService.SaveKeyArr(KeysArr); // сохранить массив ключ-точек в память
                _formView.ChangeStatus("Сгенерирован массив ключ-точек изображения");
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }
        }

        /// <summary>
        /// Получить лист кластеров
        /// </summary>
        /// <param name="countClust"></param>
        public void GetClustList(int countClust)
        {
            try
            {
                var copyKey = _stateStorageService.GetKeyArr();
                var ClList = _clusterServise.Clustering(copyKey, countClust);
                _stateStorageService.SaveClusterList(ClList);
                int count = 0;
                foreach (var el in ClList)
                {
                    _formView.Visual(el, ref count);
                }
                _formView.ChangeStatus("Определены новые кластеры");
                _formView.AddtoList(ClList);
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }
        }

        /// <summary>
        /// Получить лист участков интерполяции
        /// </summary>
        public void GetApproxTrack()
        {
            try
            {
                var clKey = _stateStorageService.GetClusterList();//
                var inputImg = _stateStorageService.GetImg();// получить копию исходного изображения
                var TrackContr = _imgProccesServise.GetTrajectory(clKey, inputImg); // лист с траекториями и опорными точками
                var ApproxTrack = _graphServise.GetApprox(TrackContr);
                _stateStorageService.SaveApproxList(ApproxTrack);
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }
        }

        public void GetFile()
        {
            try
            {
                _formView.ChangeStatus("Генерация файла. Ожидайте...");
                var approxArr = _stateStorageService.GetApproxList();
                var point = _interpritateServise.InterpretationOfCommands(approxArr, 200, 210, 0.0055, 0.009);    //a=0.002 b=0.001
                _excelServise.PointToFile(point);
                _formView.ChangeStatus("Файл создан. Время выполнения модели для Matlab: " + string.Format("{0:0.00}", _stateStorageService.GetMatlabTime()));
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }
        }
        public void PrintGraph()
        {
            try
            {
                var approxArr = _stateStorageService.GetApproxList();
                int count = 0;
                foreach (var el in approxArr)
                {
                    _formView.VisualGraph(el, ref count);
                }
                var clKey = _stateStorageService.GetClusterList();
                int count2 = count;
                foreach (var el in clKey)
                {
                    _formView.VisualGraphKey(el, ref count2);
                }
                _formView.ChangeStatus("Построенны траектории движения. Выберите желаемый тип обратотки и обновите");
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }

        }
        public void PrintGraphApprox()
        {
            try
            {
                var approxArr = _stateStorageService.GetApproxList();
                int count = 0;
                foreach (var el in approxArr)
                {
                    _formView.VisualGraphLinear(el, ref count);
                }
                var clKey = _stateStorageService.GetClusterList();
                int count2 = count;
                foreach (var el in clKey)
                {
                    _formView.VisualGraphKey(el, ref count2);
                }
                _formView.ChangeStatus("Построенны траектории движения. Выберите желаемый тип обратотки и обновите");
            }
            catch (Exception ex)
            {
                _messageServise.ShowError(ex.Message + " Изображение не было введено в систему.");
            }
        }
    }
    
}
