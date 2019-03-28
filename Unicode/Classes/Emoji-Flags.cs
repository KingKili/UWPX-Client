// This class is a part of the fork from neosmarts unicode.net (https://github.com/neosmart/unicode.net)
// Source: https://github.com/UWPX/unicode.net
// Original license:
// MIT License:
// https://github.com/UWPX/unicode.net/blob/master/LICENSE

using System.Collections.Generic;

namespace NeoSmart.Unicode
{
    // This file is machine-generated based on the official Unicode Consortium publication (https://unicode.org/Public/emoji/12.0/emoji-test.txt).
    // See https://github.com/UWPX/Emoji-List-Parser for the generator.
    public static partial class Emoji
    {
        /// <summary>
        /// A (sorted) enumeration of all emoji in group: FLAGS
        /// Only contains fully-qualified and component emoji.
        /// <summary>
#if NET20 || NET30 || NET35
        public static readonly List<SingleEmoji> Flags = new List<SingleEmoji>() {
#else
        public static SortedSet<SingleEmoji> Flags => new SortedSet<SingleEmoji>() {
#endif
            /* 🏁 */ ChequeredFlag,
            /* 🚩 */ TriangularFlag,
            /* 🎌 */ CrossedFlags,
            /* 🏴 */ BlackFlag,
            /* 🏳️ */ WhiteFlag,
            /* 🏳️‍🌈 */ RainbowFlag,
            /* 🏴‍☠️ */ PirateFlag,
            /* 🇦🇨 */ FlagAscensionIsland,
            /* 🇦🇩 */ FlagAndorra,
            /* 🇦🇪 */ FlagUnitedArabEmirates,
            /* 🇦🇫 */ FlagAfghanistan,
            /* 🇦🇬 */ FlagAntiguaBarbuda,
            /* 🇦🇮 */ FlagAnguilla,
            /* 🇦🇱 */ FlagAlbania,
            /* 🇦🇲 */ FlagArmenia,
            /* 🇦🇴 */ FlagAngola,
            /* 🇦🇶 */ FlagAntarctica,
            /* 🇦🇷 */ FlagArgentina,
            /* 🇦🇸 */ FlagAmericanSamoa,
            /* 🇦🇹 */ FlagAustria,
            /* 🇦🇺 */ FlagAustralia,
            /* 🇦🇼 */ FlagAruba,
            /* 🇦🇽 */ FlagÅlandIslands,
            /* 🇦🇿 */ FlagAzerbaijan,
            /* 🇧🇦 */ FlagBosniaHerzegovina,
            /* 🇧🇧 */ FlagBarbados,
            /* 🇧🇩 */ FlagBangladesh,
            /* 🇧🇪 */ FlagBelgium,
            /* 🇧🇫 */ FlagBurkinaFaso,
            /* 🇧🇬 */ FlagBulgaria,
            /* 🇧🇭 */ FlagBahrain,
            /* 🇧🇮 */ FlagBurundi,
            /* 🇧🇯 */ FlagBenin,
            /* 🇧🇱 */ FlagStBarthélemy,
            /* 🇧🇲 */ FlagBermuda,
            /* 🇧🇳 */ FlagBrunei,
            /* 🇧🇴 */ FlagBolivia,
            /* 🇧🇶 */ FlagCaribbeanNetherlands,
            /* 🇧🇷 */ FlagBrazil,
            /* 🇧🇸 */ FlagBahamas,
            /* 🇧🇹 */ FlagBhutan,
            /* 🇧🇻 */ FlagBouvetIsland,
            /* 🇧🇼 */ FlagBotswana,
            /* 🇧🇾 */ FlagBelarus,
            /* 🇧🇿 */ FlagBelize,
            /* 🇨🇦 */ FlagCanada,
            /* 🇨🇨 */ FlagCocosKeelingIslands,
            /* 🇨🇩 */ FlagCongoKinshasa,
            /* 🇨🇫 */ FlagCentralAfricanRepublic,
            /* 🇨🇬 */ FlagCongoBrazzaville,
            /* 🇨🇭 */ FlagSwitzerland,
            /* 🇨🇮 */ FlagCôteDivoire,
            /* 🇨🇰 */ FlagCookIslands,
            /* 🇨🇱 */ FlagChile,
            /* 🇨🇲 */ FlagCameroon,
            /* 🇨🇳 */ FlagChina,
            /* 🇨🇴 */ FlagColombia,
            /* 🇨🇵 */ FlagClippertonIsland,
            /* 🇨🇷 */ FlagCostaRica,
            /* 🇨🇺 */ FlagCuba,
            /* 🇨🇻 */ FlagCapeVerde,
            /* 🇨🇼 */ FlagCuraçao,
            /* 🇨🇽 */ FlagChristmasIsland,
            /* 🇨🇾 */ FlagCyprus,
            /* 🇨🇿 */ FlagCzechia,
            /* 🇩🇪 */ FlagGermany,
            /* 🇩🇬 */ FlagDiegoGarcia,
            /* 🇩🇯 */ FlagDjibouti,
            /* 🇩🇰 */ FlagDenmark,
            /* 🇩🇲 */ FlagDominica,
            /* 🇩🇴 */ FlagDominicanRepublic,
            /* 🇩🇿 */ FlagAlgeria,
            /* 🇪🇦 */ FlagCeutaMelilla,
            /* 🇪🇨 */ FlagEcuador,
            /* 🇪🇪 */ FlagEstonia,
            /* 🇪🇬 */ FlagEgypt,
            /* 🇪🇭 */ FlagWesternSahara,
            /* 🇪🇷 */ FlagEritrea,
            /* 🇪🇸 */ FlagSpain,
            /* 🇪🇹 */ FlagEthiopia,
            /* 🇪🇺 */ FlagEuropeanUnion,
            /* 🇫🇮 */ FlagFinland,
            /* 🇫🇯 */ FlagFiji,
            /* 🇫🇰 */ FlagFalklandIslands,
            /* 🇫🇲 */ FlagMicronesia,
            /* 🇫🇴 */ FlagFaroeIslands,
            /* 🇫🇷 */ FlagFrance,
            /* 🇬🇦 */ FlagGabon,
            /* 🇬🇧 */ FlagUnitedKingdom,
            /* 🇬🇩 */ FlagGrenada,
            /* 🇬🇪 */ FlagGeorgia,
            /* 🇬🇫 */ FlagFrenchGuiana,
            /* 🇬🇬 */ FlagGuernsey,
            /* 🇬🇭 */ FlagGhana,
            /* 🇬🇮 */ FlagGibraltar,
            /* 🇬🇱 */ FlagGreenland,
            /* 🇬🇲 */ FlagGambia,
            /* 🇬🇳 */ FlagGuinea,
            /* 🇬🇵 */ FlagGuadeloupe,
            /* 🇬🇶 */ FlagEquatorialGuinea,
            /* 🇬🇷 */ FlagGreece,
            /* 🇬🇸 */ FlagSouthGeorgiaSouthSandwichIslands,
            /* 🇬🇹 */ FlagGuatemala,
            /* 🇬🇺 */ FlagGuam,
            /* 🇬🇼 */ FlagGuineaBissau,
            /* 🇬🇾 */ FlagGuyana,
            /* 🇭🇰 */ FlagHongKongSarChina,
            /* 🇭🇲 */ FlagHeardMcdonaldIslands,
            /* 🇭🇳 */ FlagHonduras,
            /* 🇭🇷 */ FlagCroatia,
            /* 🇭🇹 */ FlagHaiti,
            /* 🇭🇺 */ FlagHungary,
            /* 🇮🇨 */ FlagCanaryIslands,
            /* 🇮🇩 */ FlagIndonesia,
            /* 🇮🇪 */ FlagIreland,
            /* 🇮🇱 */ FlagIsrael,
            /* 🇮🇲 */ FlagIsleMan,
            /* 🇮🇳 */ FlagIndia,
            /* 🇮🇴 */ FlagBritishIndianOceanTerritory,
            /* 🇮🇶 */ FlagIraq,
            /* 🇮🇷 */ FlagIran,
            /* 🇮🇸 */ FlagIceland,
            /* 🇮🇹 */ FlagItaly,
            /* 🇯🇪 */ FlagJersey,
            /* 🇯🇲 */ FlagJamaica,
            /* 🇯🇴 */ FlagJordan,
            /* 🇯🇵 */ FlagJapan,
            /* 🇰🇪 */ FlagKenya,
            /* 🇰🇬 */ FlagKyrgyzstan,
            /* 🇰🇭 */ FlagCambodia,
            /* 🇰🇮 */ FlagKiribati,
            /* 🇰🇲 */ FlagComoros,
            /* 🇰🇳 */ FlagStKittsNevis,
            /* 🇰🇵 */ FlagNorthKorea,
            /* 🇰🇷 */ FlagSouthKorea,
            /* 🇰🇼 */ FlagKuwait,
            /* 🇰🇾 */ FlagCaymanIslands,
            /* 🇰🇿 */ FlagKazakhstan,
            /* 🇱🇦 */ FlagLaos,
            /* 🇱🇧 */ FlagLebanon,
            /* 🇱🇨 */ FlagStLucia,
            /* 🇱🇮 */ FlagLiechtenstein,
            /* 🇱🇰 */ FlagSriLanka,
            /* 🇱🇷 */ FlagLiberia,
            /* 🇱🇸 */ FlagLesotho,
            /* 🇱🇹 */ FlagLithuania,
            /* 🇱🇺 */ FlagLuxembourg,
            /* 🇱🇻 */ FlagLatvia,
            /* 🇱🇾 */ FlagLibya,
            /* 🇲🇦 */ FlagMorocco,
            /* 🇲🇨 */ FlagMonaco,
            /* 🇲🇩 */ FlagMoldova,
            /* 🇲🇪 */ FlagMontenegro,
            /* 🇲🇫 */ FlagStMartin,
            /* 🇲🇬 */ FlagMadagascar,
            /* 🇲🇭 */ FlagMarshallIslands,
            /* 🇲🇰 */ FlagMacedonia,
            /* 🇲🇱 */ FlagMali,
            /* 🇲🇲 */ FlagMyanmarBurma,
            /* 🇲🇳 */ FlagMongolia,
            /* 🇲🇴 */ FlagMacaoSarChina,
            /* 🇲🇵 */ FlagNorthernMarianaIslands,
            /* 🇲🇶 */ FlagMartinique,
            /* 🇲🇷 */ FlagMauritania,
            /* 🇲🇸 */ FlagMontserrat,
            /* 🇲🇹 */ FlagMalta,
            /* 🇲🇺 */ FlagMauritius,
            /* 🇲🇻 */ FlagMaldives,
            /* 🇲🇼 */ FlagMalawi,
            /* 🇲🇽 */ FlagMexico,
            /* 🇲🇾 */ FlagMalaysia,
            /* 🇲🇿 */ FlagMozambique,
            /* 🇳🇦 */ FlagNamibia,
            /* 🇳🇨 */ FlagNewCaledonia,
            /* 🇳🇪 */ FlagNiger,
            /* 🇳🇫 */ FlagNorfolkIsland,
            /* 🇳🇬 */ FlagNigeria,
            /* 🇳🇮 */ FlagNicaragua,
            /* 🇳🇱 */ FlagNetherlands,
            /* 🇳🇴 */ FlagNorway,
            /* 🇳🇵 */ FlagNepal,
            /* 🇳🇷 */ FlagNauru,
            /* 🇳🇺 */ FlagNiue,
            /* 🇳🇿 */ FlagNewZealand,
            /* 🇴🇲 */ FlagOman,
            /* 🇵🇦 */ FlagPanama,
            /* 🇵🇪 */ FlagPeru,
            /* 🇵🇫 */ FlagFrenchPolynesia,
            /* 🇵🇬 */ FlagPapuaNewGuinea,
            /* 🇵🇭 */ FlagPhilippines,
            /* 🇵🇰 */ FlagPakistan,
            /* 🇵🇱 */ FlagPoland,
            /* 🇵🇲 */ FlagStPierreMiquelon,
            /* 🇵🇳 */ FlagPitcairnIslands,
            /* 🇵🇷 */ FlagPuertoRico,
            /* 🇵🇸 */ FlagPalestinianTerritories,
            /* 🇵🇹 */ FlagPortugal,
            /* 🇵🇼 */ FlagPalau,
            /* 🇵🇾 */ FlagParaguay,
            /* 🇶🇦 */ FlagQatar,
            /* 🇷🇪 */ FlagRéunion,
            /* 🇷🇴 */ FlagRomania,
            /* 🇷🇸 */ FlagSerbia,
            /* 🇷🇺 */ FlagRussia,
            /* 🇷🇼 */ FlagRwanda,
            /* 🇸🇦 */ FlagSaudiArabia,
            /* 🇸🇧 */ FlagSolomonIslands,
            /* 🇸🇨 */ FlagSeychelles,
            /* 🇸🇩 */ FlagSudan,
            /* 🇸🇪 */ FlagSweden,
            /* 🇸🇬 */ FlagSingapore,
            /* 🇸🇭 */ FlagStHelena,
            /* 🇸🇮 */ FlagSlovenia,
            /* 🇸🇯 */ FlagSvalbardJanMayen,
            /* 🇸🇰 */ FlagSlovakia,
            /* 🇸🇱 */ FlagSierraLeone,
            /* 🇸🇲 */ FlagSanMarino,
            /* 🇸🇳 */ FlagSenegal,
            /* 🇸🇴 */ FlagSomalia,
            /* 🇸🇷 */ FlagSuriname,
            /* 🇸🇸 */ FlagSouthSudan,
            /* 🇸🇹 */ FlagSãoToméPríncipe,
            /* 🇸🇻 */ FlagElSalvador,
            /* 🇸🇽 */ FlagSintMaarten,
            /* 🇸🇾 */ FlagSyria,
            /* 🇸🇿 */ FlagEswatini,
            /* 🇹🇦 */ FlagTristanDaCunha,
            /* 🇹🇨 */ FlagTurksCaicosIslands,
            /* 🇹🇩 */ FlagChad,
            /* 🇹🇫 */ FlagFrenchSouthernTerritories,
            /* 🇹🇬 */ FlagTogo,
            /* 🇹🇭 */ FlagThailand,
            /* 🇹🇯 */ FlagTajikistan,
            /* 🇹🇰 */ FlagTokelau,
            /* 🇹🇱 */ FlagTimorLeste,
            /* 🇹🇲 */ FlagTurkmenistan,
            /* 🇹🇳 */ FlagTunisia,
            /* 🇹🇴 */ FlagTonga,
            /* 🇹🇷 */ FlagTurkey,
            /* 🇹🇹 */ FlagTrinidadTobago,
            /* 🇹🇻 */ FlagTuvalu,
            /* 🇹🇼 */ FlagTaiwan,
            /* 🇹🇿 */ FlagTanzania,
            /* 🇺🇦 */ FlagUkraine,
            /* 🇺🇬 */ FlagUganda,
            /* 🇺🇲 */ FlagUsOutlyingIslands,
            /* 🇺🇳 */ FlagUnitedNations,
            /* 🇺🇸 */ FlagUnitedStates,
            /* 🇺🇾 */ FlagUruguay,
            /* 🇺🇿 */ FlagUzbekistan,
            /* 🇻🇦 */ FlagVaticanCity,
            /* 🇻🇨 */ FlagStVincentGrenadines,
            /* 🇻🇪 */ FlagVenezuela,
            /* 🇻🇬 */ FlagBritishVirginIslands,
            /* 🇻🇮 */ FlagUsVirginIslands,
            /* 🇻🇳 */ FlagVietnam,
            /* 🇻🇺 */ FlagVanuatu,
            /* 🇼🇫 */ FlagWallisFutuna,
            /* 🇼🇸 */ FlagSamoa,
            /* 🇽🇰 */ FlagKosovo,
            /* 🇾🇪 */ FlagYemen,
            /* 🇾🇹 */ FlagMayotte,
            /* 🇿🇦 */ FlagSouthAfrica,
            /* 🇿🇲 */ FlagZambia,
            /* 🇿🇼 */ FlagZimbabwe,
            /* 🏴󠁧󠁢󠁥󠁮󠁧󠁿 */ FlagEngland,
            /* 🏴󠁧󠁢󠁳󠁣󠁴󠁿 */ FlagScotland,
            /* 🏴󠁧󠁢󠁷󠁬󠁳󠁿 */ FlagWales,
        };
    }
}
