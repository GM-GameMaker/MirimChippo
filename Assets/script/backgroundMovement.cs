using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ�� ����
    public float backgroundSpeed = 0.1f; // ����� ������ �ӵ� (0.1f�� ����)

    private Vector3 lastPlayerPosition;

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("�÷��̾� ������Ʈ�� �������� �ʾҽ��ϴ�. BackgroundMovement ��ũ��Ʈ�� �÷��̾� ������Ʈ�� �������ּ���.");
        }
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // �÷��̾��� ��ġ ��ȭ ���
        Vector3 playerMovement = player.transform.position - lastPlayerPosition;
        lastPlayerPosition = player.transform.position;

        // ��� ������Ʈ�� ��ġ ���� (�÷��̾� �̵� ���� �ݴ��)
        transform.position -= playerMovement * backgroundSpeed;
    }
}