using UnityEngine;

public class ChessboardGenerator : MonoBehaviour
{
    public GameObject whiteTilePrefab;  // ��� Ÿ�� ������
    public GameObject blackTilePrefab;  // ������ Ÿ�� ������
    public int boardSize = 8;
    public Vector2 tileSize = new Vector2(1, 1);  // Ÿ�� ũ�� ����

    void Start()
    {
        GenerateChessboard();
    }

    void GenerateChessboard()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                // ü���� ������ ���� x�� y�� ���� ¦���� ��� ��� Ÿ��, Ȧ���� ��� ������ Ÿ��
                GameObject tilePrefab = (x + y) % 2 == 0 ? whiteTilePrefab : blackTilePrefab;

                // Ÿ�� ����
                Vector3 position = new Vector3(x * tileSize.x + 0.5f - 5, y * tileSize.y + 0.5f - 5, 0);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
