namespace TXTSoccer.helpers
{
	/// <summary>
	/// Uma classe para mostrar um menu de seleção no terminal.
	/// </summary>
	internal class Select
	{
		public static Select? instance = null;
		public static Select Instance
		{
			get
			{
				instance ??= new Select();
				return instance;
			}
		}

		/// <summary>
		/// Escreve um menu de seleção no terminal.
		/// </summary>
		/// <param name="options">Lista de opções disponíveis</param>
		/// <param name="selectionInput">Objeto de seleção</param>
		/// <returns>Um <see cref="SelectInput"/> com a tecla pressionada e o índice da opção selecionada</returns>
		public SelectInput GetChoice(List<string> options, SelectInput selectionInput)
		{
			if (selectionInput.ChoiceIndex > options.Count - 1)
				selectionInput.ChoiceIndex = 0;

			options.ForEach(c =>
			{
				string selector = options.IndexOf(c) == selectionInput.ChoiceIndex ? "->" : "-";
				Console.WriteLine($"{selector} {c}");
			});

			Console.WriteLine("\n[Setas cima e baixo] - Mudar opcao\t[ENTER] - Selecionar opcao");

			selectionInput.Key = Console.ReadKey(false).Key;

			switch (selectionInput.Key)
			{
				case ConsoleKey.UpArrow:
					if (selectionInput.ChoiceIndex > 0)
						selectionInput.ChoiceIndex -= 1;
					break;
				case ConsoleKey.DownArrow:
					if (selectionInput.ChoiceIndex < options.Count - 1)
						selectionInput.ChoiceIndex += 1;
					break;
			}

			Console.Clear();
			return selectionInput;
		}
	}

	/// <summary>
	/// Uma classe para receber os dados obtidos do <see cref="Select"/>.
	/// </summary>
	internal class SelectInput
	{
		public int ChoiceIndex {get; set;}
		public ConsoleKey? Key {get; set; }

        public static SelectInput? instance = null;
        public static SelectInput Instance
        {
            get
            {
                instance ??= new SelectInput(0, null);
                return instance;
            }
        }
        public SelectInput(int choiceIndex, ConsoleKey? key)
		{
			ChoiceIndex = choiceIndex;
			Key = key;
		}
	}
}
