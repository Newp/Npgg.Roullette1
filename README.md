# Roullette1
SignalR 과 Proto Actor 를 사용하여 구현한 실시간 룰렛서버,

### 게임방식
10초마다 룰렛이 돌아가며, 플레이어는 이때까지 계속해서 배팅할 수 있다.
베팅에 몰린 금액은 실시간으로 올라가며 플레이어들에게 배팅된 금액을 전달한다.

배팅과 관련된 자원은 Actor Model 을 통하여 Lockless 상태로 메세지패싱 방식으로 제어한다.


### Client HubConnection, Server RouletteHub
SignalR 의 Hub, http API의 controller/ action 과 비슷한 동작이다.
Client의 HubConnection 을 통해서 서버로 메세지를 보내며 
서버는 Hub를 상속한 RouletteHub 를 통하여 메세지를전달 받는다.

서버에서 메세지를 내릴때는 역순.

### SignalR의 장단점

단점-
서버측에서 커넥션이 몰릴 때 소켓에 비해 많이 느려진다.
실제로 메세지 주고 받는 속도도 소켓에 비해서는 느리다.
버전이 어렵다. ( signalR1, signalR2, signalR core1 ...)

장점- 
정말 간편하고 단순하다. 이렇게 쉬울수가 있나 싶을정도.
앞서 성능이 좋지 않다고 했지만 그래도 초당 5000~10000건 이상의 메세지를 처리할 수 있다.
redis 를 활용하여 스케일아웃이 가능하다고 들었지만 해보진 않음.
