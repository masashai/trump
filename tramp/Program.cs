using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace trump
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0;i < 5; i++)
            {
                Console.WriteLine($"{i + 1}回目----------------------");
                SortTest(100000);
            }
        }

        static void SortTest(int size)
        {
            var sw = new Stopwatch();

            // パターン1実行
            sw.Start();
            var result1 = Pattern1(size);
            //WriteResult(result1);
            sw.Stop();
            WriteTime(sw.Elapsed);

            // パターン2実行
            sw.Restart();
            var result2 = Pattern2(size);
            //WriteResult(result2);
            sw.Stop();
            WriteTime(sw.Elapsed);
        }

        static void WriteResult(List<Trump> result)
        {
            foreach (var card in result)
            {
                Console.WriteLine(string.Format("Mark: {0},Number: {1}", card.Mark, card.Number));
            }
        }

        static void WriteTime(TimeSpan ts) =>
            Console.WriteLine($"パターン1: {ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒");

        static List<Trump> Pattern1(int size)
        {
            var output = new List<Trump>();
            //var deck = Trump.GetDeck();
            var deck = Trump.GetLargeDeck(size);
            var spadeCards = new List<Trump>();
            var heartCards = new List<Trump>();
            var diamondCards = new List<Trump>();
            var clubCards = new List<Trump>();
            foreach (var card in deck)
            {
                switch (card.Mark)
                {
                    case Mark.Spade:
                        spadeCards.Add(card);
                        break;
                    case Mark.Heart:
                        heartCards.Add(card);
                        break;
                    case Mark.Diamond:
                        diamondCards.Add(card);
                        break;
                    case Mark.Club:
                        clubCards.Add(card);
                        break;
                }
            }
            spadeCards = spadeCards.OrderBy(_ => _.Number).ToList<Trump>();
            heartCards =  heartCards.OrderBy(_ => _.Number).ToList<Trump>();
            diamondCards = diamondCards.OrderBy(_ => _.Number).ToList<Trump>();
            clubCards= clubCards.OrderBy(_ => _.Number).ToList<Trump>();

            output.AddRange(spadeCards);
            output.AddRange(heartCards);
            output.AddRange(diamondCards);
            output.AddRange(clubCards);

            return output;
        }

        static List<Trump> Pattern2(int size)
        {
            var output = new List<Trump>();
            //var deck = Trump.GetDeck();
            var deck = Trump.GetLargeDeck(size);

            var blackCards = new List<Trump>();
            var redCards = new List<Trump>();

            var spadeCards = new List<Trump>();
            var heartCards = new List<Trump>();
            var diamondCards = new List<Trump>();
            var clubCards = new List<Trump>();

            foreach (var card in deck)
            {
                switch (card.Color)
                {
                    case Color.Black:
                        blackCards.Add(card);
                        break;
                    case Color.Red:
                        redCards.Add(card);
                        break;
                }
            }

            foreach (var card in blackCards)
            {
                switch (card.Mark)
                {
                    case Mark.Spade:
                        spadeCards.Add(card);
                        break;
                    case Mark.Club:
                        clubCards.Add(card);
                        break;
                }
            }

            foreach (var card in redCards)
            {
                switch (card.Mark)
                {
                    case Mark.Diamond:
                        diamondCards.Add(card);
                        break;
                    case Mark.Heart:
                        heartCards.Add(card);
                        break;
                }
            }

            spadeCards = spadeCards.OrderBy(_ => _.Number).ToList<Trump>();
            heartCards = heartCards.OrderBy(_ => _.Number).ToList<Trump>();
            diamondCards = diamondCards.OrderBy(_ => _.Number).ToList<Trump>();
            clubCards = clubCards.OrderBy(_ => _.Number).ToList<Trump>();

            return output;
        }
    }

    public class Trump
    {
        public Mark Mark;
        public int Number;
        public Color Color;

        public static List<Trump> GetDeck()
        {
            var Deck = new List<Trump>();
            foreach (Mark mark in Enum.GetValues(typeof(Mark)))
            {
                for (var n = 1; n <= 13; n++ )
                {
                    Deck.Add(new Trump()
                    {
                        Mark = mark,
                        Number = n,
                        Color = (mark == Mark.Club || mark == Mark.Spade) ? Color.Black : Color.Red
                    });
                }
            }
            return Deck.Shuffle();
        }

        public static List<Trump> GetLargeDeck(int size)
        {
            var deck = new List<Trump>();
            for (var i = 0; i < size; i++)
            {
                deck.AddRange(GetDeck());
            }
            //Console.WriteLine("deck count: " + deck.Count);
            return deck;
        }
    }

    static class TrumpExtentions
    {
        public static List<Trump> Shuffle(this List<Trump> deck)
        {
            return deck.OrderBy(_ => Guid.NewGuid()).ToList<Trump>();
        }
    }

    public enum Mark
    {
        Spade,
        Heart,
        Diamond,
        Club
    }

    public enum Color
    {
        Black,
        Red
    }
}
