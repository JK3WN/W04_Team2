using UnityEngine;

public class ChessboardGenerator : MonoBehaviour
{
    public GameObject whiteTilePrefab;  // 흰색 타일 프리팹
    public GameObject blackTilePrefab;  // 검은색 타일 프리팹
    public int boardSize = 8;
    public Vector2 tileSize = new Vector2(1, 1);  // 타일 크기 설정

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
                // 체스판 패턴을 위해 x와 y의 합이 짝수인 경우 흰색 타일, 홀수인 경우 검은색 타일
                GameObject tilePrefab = (x + y) % 2 == 0 ? whiteTilePrefab : blackTilePrefab;

                // 타일 생성
                Vector3 position = new Vector3(x * tileSize.x + 0.5f - 5, y * tileSize.y + 0.5f - 5, 0);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
