using UnityEngine;

//by.J:230720 ����ǰ �������̽�(�԰�) -> ������Ƽ�� �б�����
//by.J:230808 �̹��� �߰�
//by.J:230811 ���� ID �߰�
//by.J:230814 �迭 -> ���Ϸ� ����
public interface IItem
{
    string ItemName { get; }    //�̸�
    int ItemCost { get; }       //����
    Sprite ItemImage { get; }   //�̹���
    string ItemId { get; }      //���� ID

}
