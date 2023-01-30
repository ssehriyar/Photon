using NewUtils;
using TMPro;
using UnityEngine;

namespace MyGame
{
	public class ScoreManager : Singleton<ScoreManager>
	{
		[SerializeField] private TMP_Text _player1ScoreText;
		[SerializeField] private TMP_Text _player2ScoreText;
		public int Player1Score { get; private set; }
		public int Player2Score { get; private set; }

		private void Awake()
		{
			Init();
			Player1Score = 0;
			Player2Score = 0;
			_player2ScoreText.text = Player2Score.ToString();
			_player1ScoreText.text = Player1Score.ToString();
		}

		public int AddScore(PlayerName player)
		{
			switch (player)
			{
				case PlayerName.Player1:
					++Player2Score;
					_player2ScoreText.text = Player2Score.ToString();
					return Player2Score;
				case PlayerName.Player2:
					++Player1Score;
					_player1ScoreText.text = Player1Score.ToString();
					return Player1Score;
				default:
					return -1;
			}
		}
	}
}