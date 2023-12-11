namespace CardGames.Domain;

public class CardDeck : ICardDeck
{
    private readonly List<ICard> _cards;
    private static readonly Random Random = new Random();

    public CardDeck()
    {
        _cards = CreateCards().ToList();
        Shuffle();
    }

    public CardDeck(IEnumerable<ICard> cards)
    {
        _cards = cards.ToList();
    }

    public int RemainingCards => _cards.Count;

    public void Shuffle()
    {
        for (var index = 0; index < _cards.Count; index++)
        {
            var randomIndex = Random.Next(index, _cards.Count);
            (_cards[index], _cards[randomIndex]) = (_cards[randomIndex], _cards[index]);
        }
    }

    public ICard DealCard()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("No more cards to deal.");
        }

        var card = _cards[_cards.Count - 1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }

    public ICardDeck WithoutCardsRankingLowerThan(CardRank minimumRank)
    {
        return new CardDeck(GetCards().Where(card => card.Rank >= minimumRank));
    }

    public IList<CardDeck> SplitBySuit()
    {
        return _cards.GroupBy(card => card.Suit)
            .Select(group => new CardDeck(group.ToList()))
            .ToList();
    }

    private IEnumerable<ICard> CreateCards()
    {
        foreach (CardSuit suit in Enum.GetValues<CardSuit>())
        {
            foreach (CardRank rank in Enum.GetValues<CardRank>())
            {
                yield return new Card(suit, rank);
            }
        }
    }

    private IEnumerable<ICard> GetCards()
    {
        return _cards;
    }
}