using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;

namespace CalamityMod.DataStructures;

public class SummonTag
{
	public delegate void ModifyHitByProjectileTag(Projectile proj, NPC npc, ref NPC.HitModifiers modifiers, ref float tagDamageMult, ref float critChance);

	public delegate void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone);

	public int FlatTagDamage;

	public int UnlistedTagDamage;

	public float MultiplicativeTagDamage;

	public float UnlistedMultiplicativeTagDamage;

	public float TagCritChance;

	public float TagCritDamage;

	public Asset<Texture2D> TagTexture;

	public int TagItem = -1;

	public ModifyHitByProjectileTag TagModifyHitEffects;

	public bool AutoDrawTooltip = true;

	public bool AllowsWhipStacking;

	public OnHitByProjectile TagOnHit = BlankOnHit;

	public static SummonTag LeatherWhip = new SummonTag(4672)
	{
		FlatTagDamage = 2,
		UnlistedTagDamage = -4
	};

	public static SummonTag Snapthorn = new SummonTag(4913)
	{
		FlatTagDamage = 1,
		UnlistedTagDamage = -6
	};

	public static SummonTag SpinalTap = new SummonTag(5074)
	{
		FlatTagDamage = 4,
		UnlistedTagDamage = -7
	};

	public static SummonTag Firecracker = new SummonTag(4912)
	{
		TagCritChance = 1f,
		UnlistedMultiplicativeTagDamage = -1.75f,
		AutoDrawTooltip = false
	};

	public static SummonTag CoolWhip = new SummonTag(4911)
	{
		FlatTagDamage = 6,
		UnlistedTagDamage = -6
	};

	public static SummonTag Durendal = new SummonTag(4678)
	{
		FlatTagDamage = 4,
		TagCritChance = 0.05f,
		UnlistedTagDamage = -9
	};

	public static SummonTag MorningStar = new SummonTag(4679)
	{
		TagCritChance = 0.13f,
		UnlistedTagDamage = -8
	};

	public static SummonTag DarkHarvest = new SummonTag(4680)
	{
		FlatTagDamage = 10,
		UnlistedTagDamage = -10
	};

	public static SummonTag Kaleidoscope = new SummonTag(4914)
	{
		FlatTagDamage = 8,
		TagCritChance = 0.1f,
		UnlistedTagDamage = -20
	};

	public SummonTag()
	{
		TagModifyHitEffects = ApplyTagModifyHit;
	}

	public SummonTag(int itemType)
	{
		TagItem = itemType;
		TagModifyHitEffects = ApplyTagModifyHit;
	}

	public void ApplyTagModifyHit(Projectile proj, NPC npc, ref NPC.HitModifiers modifiers, ref float tagDamageMult, ref float critChance)
	{
		modifiers.FlatBonusDamage += (float)(FlatTagDamage + UnlistedTagDamage) * tagDamageMult;
		modifiers.ScalingBonusDamage += (MultiplicativeTagDamage + UnlistedMultiplicativeTagDamage) * tagDamageMult;
		critChance += TagCritChance;
		modifiers.CritDamage += TagCritDamage;
	}

	public static void BlankTagModifyHit(Projectile proj, NPC npc, ref NPC.HitModifiers modifiers, ref float tagDamageMult, ref float critChance)
	{
	}

	public static void BlankOnHit(NPC npc, Projectile projectile, NPC.HitInfo hit, int damagedone)
	{
	}
}
