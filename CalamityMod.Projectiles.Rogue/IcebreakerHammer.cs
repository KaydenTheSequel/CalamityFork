using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class IcebreakerHammer : ModProjectile, ILocalizedModType, IModType
{
	private int explosionCount;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Icebreaker";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.aiStyle = 3;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 600;
		base.AIType = 52;
		base.Projectile.coldDamage = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		target.AddBuff(324, 180);
		if (!base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		if (explosionCount < 3)
		{
			int ice = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CosmicIceBurst>(), (int)((double)base.Projectile.damage * 1.5), base.Projectile.knockBack, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f, 1f);
			if (ice.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[ice].DamageType = RogueDamageClass.Instance;
			}
			explosionCount++;
		}
		int buffType = ModContent.BuffType<GlacialState>();
		float radius = 112f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC nPC = enumerator.Current;
			if (!nPC.dontTakeDamage && !nPC.buffImmune[buffType] && Vector2.Distance(base.Projectile.Center, nPC.Center) <= radius && nPC.FindBuffIndex(buffType) == -1)
			{
				nPC.AddBuff(buffType, 60);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		target.AddBuff(324, 180);
		if (base.Projectile.Calamity().stealthStrike)
		{
			int ice = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<CosmicIceBurst>(), (int)((double)base.Projectile.damage * 1.5), base.Projectile.knockBack, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f, 1f);
			if (ice.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[ice].DamageType = RogueDamageClass.Instance;
			}
		}
	}
}
