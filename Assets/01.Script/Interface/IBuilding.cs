
//by.J:230719 건물 인터페이스(규격) -> 프로퍼티로 읽기전용
public interface IBuilding
{
    string [] BuildingName { get; }     //이름
    int [] Buildingcost { get; }        //가격
    float [] BuildingBuildTime { get; } //건축 시간
}   

