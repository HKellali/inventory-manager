using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        KnifeOfSolitude,
        BowToYourKing,
        SpearMeTheDetails,
        ShovelUpYourAsh,
        JustASword,
        AxeMeAQuestion,
        PickaxeOfDespair,
        ImpressiveSimitar,
        Arrow,
        CovidArrow,
        NotVeryExpensiveCape,
        FakeArmor,
        RealBigArmor,
        AntiVirus,
        GreatBootsIfISaySoMyself,
        NogginCover,
        MichaelCane,
        ExhaustingWand,
        WillYouMarryMe,
        AmuLetMeSleep,
        DietCoke,
        RegularCoke,
        Pepsi,
        Poison,
        Peenuts,
        CanIRubyYou,
        MyDirtySecrets,
        KeyToMyHeart,
        MeatMeThere,
        DoctorsHateHim,
        StoreBoughtCarrot,
        NeedlesslyExpensiveFlower,
        TalkingFish,
        SaveGame,
        Settings,
        WhatSheSaysVersusWhatYouHear,
        StillBeatingHeart,
        WaveOfRelief,
        StegosaurusSpikes,
        NotAChickenDrumstick,
        SwipeLeft,
        SwipeRight,
        TheFriendZone,
        Blocked,
        IAmAngry,
        ISeeYou,
        DontCrossMe,
        TheNeedle,
        YouCantSeeMe,
        NowYouCan,
        OhNoMyEyes,
        Flubber,
        I,
        Am,
        Tired,
    }
    public ItemType itemType;

    public Sprite GetSprite()
    {
        int number = (int)itemType;
        return ItemList.Instance.sprites[number];
    }
}

