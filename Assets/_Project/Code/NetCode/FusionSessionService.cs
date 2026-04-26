using Fusion;
using Zenject;

namespace NetCode
{
    //TODO: бинд в AppBootstrapper/EntryPoint для mainMenu/кнопки в MainMenu
    public class FusionSessionService
    {
        //TODO: Создание/взятие извне + забиндить
        private NetworkRunner _networkRunner;
        
        //TODO: Сделать RoomRegistry? Чтоб прощё было с игроками(как в сетевухе, так и с геймплеем)
        
        [Inject]
        private void Construct(NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
        }
        
        public void CreateRoom(string roomName)
        {
            //TODO: Реаилзация через LobbyService?
        }

        public void JoinRoom(string roomName)
        {
            //TODO: Реаилзация c проверкой на возможность подключится через LobbyService?
        }

        //TODO: Разобраться с чуваком
        //public void CanJoinRoom(string roomName, string nickname) { return BannedPlayersService.CanJoin(roomName, nickname); }

        public void Shutdown()
        {
            //TODO: Реаилзация 
        }
    }
}