
//by.J:230720 농장밭 인터페이스(규격) -> 프로퍼티로 읽기전용
public interface IFarm
{
    string[] FarmName { get; }    //이름
    int[] FarmCost { get; }       //가격
    int[] FarmHaverst  { get; }   //작물 수확량
    float[] FarmGrowTime { get; } //작물 성장 시간

}