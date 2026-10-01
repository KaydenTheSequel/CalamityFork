using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class IceBarrageMain : ModProjectile, ILocalizedModType, IModType
{
	private const int pwidth = 58;

	private const int pheight = 58;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 58;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 280;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		NPC closestTarget = base.Projectile.Center.ClosestNPCAt(5000f, ignoreTiles: true, bossPriority: true);
		if (closestTarget != null)
		{
			base.Projectile.Center = closestTarget.Center;
		}
		Timer++;
		for (int j = 0; j < 3; j++)
		{
			int dustType = (Main.rand.NextBool() ? 68 : (Main.rand.NextBool(4) ? 80 : 67));
			if (Timer < 140f)
			{
				Dust.NewDustPerfect(base.Projectile.Center, dustType, Main.rand.NextVector2Circular(-4f, 4f), 50, default(Color), 1.3f).noGravity = true;
				continue;
			}
			int direct = (Main.rand.NextBool() ? 1 : (-1));
			Vector2 position = base.Projectile.position + new Vector2((float)Main.rand.Next(base.Projectile.width), (float)Main.rand.Next(base.Projectile.height));
			Dust.NewDustPerfect(position, dustType, Vector2.UnitY * 10f * (float)direct, 50, default(Color), 1.3f).noGravity = true;
			direct = (Main.rand.NextBool() ? 1 : (-1));
			Dust.NewDustPerfect(position, dustType, Vector2.UnitX * 10f * (float)direct, 50, default(Color), 1.3f).noGravity = true;
		}
		if (Timer < 55f)
		{
			for (int i = 0; i < 9; i++)
			{
				int auraDustType = (Main.rand.NextBool() ? 68 : (Main.rand.NextBool(4) ? 80 : 67));
				Vector2 auraDustPos = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(250f, 270f);
				Vector2 auraDustSpeed = Vector2.Normalize(base.Projectile.Center - auraDustPos) * 0.5f;
				Vector2? velocity = auraDustSpeed;
				float scale = Main.rand.NextFloat(1.5f, 2f);
				Dust.NewDustPerfect(auraDustPos, auraDustType, velocity, 0, default(Color), scale).noGravity = true;
			}
		}
		else if (Timer == 55f)
		{
			for (int k = 0; k < 210; k++)
			{
				int inwardDustType = (Main.rand.NextBool() ? 68 : (Main.rand.NextBool(4) ? 80 : 67));
				Vector2 inwardDustPos = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(250f, 270f);
				Vector2 inwardDustSpeed = Vector2.Normalize(base.Projectile.Center - inwardDustPos) * Main.rand.NextFloat(8f, 34f);
				Vector2? velocity2 = inwardDustSpeed;
				float scale = Main.rand.NextFloat(1.5f, 2f);
				Dust.NewDustPerfect(inwardDustPos, inwardDustType, velocity2, 0, default(Color), scale).noGravity = true;
			}
		}
		else if (Timer == 140f)
		{
			Vector2 projcenter = base.Projectile.Center;
			base.Projectile.width = 200;
			base.Projectile.height = 200;
			base.Projectile.Center = projcenter;
			base.Projectile.Damage();
			for (int l = 0; l < 150; l++)
			{
				int outwardDustType = (Main.rand.NextBool() ? 68 : (Main.rand.NextBool(4) ? 80 : 67));
				Dust.NewDustPerfect(base.Projectile.Center, outwardDustType, Main.rand.NextVector2Circular(-18f, 18f), 50, default(Color), 1.5f).noGravity = true;
			}
			base.Projectile.width = 58;
			base.Projectile.height = 58;
			base.Projectile.Center = projcenter;
			Vector2 pos1 = default(Vector2);
			((Vector2)(ref pos1))._002Ector(base.Projectile.Center.X, base.Projectile.Center.Y + (float)base.Projectile.height * 0.5f + 20f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos1, Vector2.Zero, ModContent.ProjectileType<IceBlock>(), (int)((float)base.Projectile.damage * 0.3f), 5f, base.Projectile.owner);
			Vector2 pos2 = default(Vector2);
			((Vector2)(ref pos2))._002Ector(base.Projectile.Center.X - (float)base.Projectile.width * 0.5f - 20f, base.Projectile.Center.Y);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos2, Vector2.Zero, ModContent.ProjectileType<IceBlock>(), (int)((float)base.Projectile.damage * 0.3f), 5f, base.Projectile.owner, 1f);
			Vector2 pos3 = default(Vector2);
			((Vector2)(ref pos3))._002Ector(base.Projectile.Center.X, base.Projectile.Center.Y - (float)base.Projectile.height * 0.5f - 20f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos3, Vector2.Zero, ModContent.ProjectileType<IceBlock>(), (int)((float)base.Projectile.damage * 0.3f), 5f, base.Projectile.owner, 2f);
			Vector2 pos4 = default(Vector2);
			((Vector2)(ref pos4))._002Ector(base.Projectile.Center.X + (float)base.Projectile.width * 0.5f + 20f, base.Projectile.Center.Y);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos4, Vector2.Zero, ModContent.ProjectileType<IceBlock>(), (int)((float)base.Projectile.damage * 0.3f), 5f, base.Projectile.owner, 3f);
		}
		if (!(Timer > 90f))
		{
			return;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 5f)
		{
			Vector2 spawnPos = base.Projectile.Center + new Vector2(Main.rand.NextFloat(-200f, 200f), -400f);
			int ice = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, Vector2.UnitY * 6f, 344, (int)((float)base.Projectile.damage * 0.05f), 2f, base.Projectile.owner, 0f, Main.rand.Next(3));
			if (ice.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[ice].tileCollide = false;
				Main.projectile[ice].DamageType = DamageClass.Magic;
			}
			base.Projectile.ai[1] = 0f;
		}
	}

	public override bool? CanDamage()
	{
		if (Timer != 140f)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
	}
}
