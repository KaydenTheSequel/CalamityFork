using System.Linq;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.extraUpdates = 0;
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, new Vector3(0.075f, 0.5f, 0.15f));
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.985f;
		base.Projectile.rotation += base.Projectile.velocity.X * 0.2f;
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.rotation += 0.08f;
		}
		else
		{
			base.Projectile.rotation -= 0.08f;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > 30f)
		{
			base.Projectile.alpha += 10;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.alpha = 255;
				base.Projectile.Kill();
				return;
			}
		}
		int chance = 24 * base.Projectile.MaxUpdates;
		if (!Main.rand.NextBool(chance))
		{
			return;
		}
		int MaxLaserCountPerShot = 5;
		int targetCount = 0;
		foreach (NPC target in Main.npc.Where(delegate(NPC npc)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return npc.active && base.Projectile.Distance(npc.Center) < 500f && npc.CanBeChasedBy();
		}).ToList())
		{
			if (targetCount >= MaxLaserCountPerShot)
			{
				break;
			}
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), (int)((float)base.Projectile.damage * 0.8f), base.Projectile.knockBack, base.Projectile.owner, target.whoAmI).ArmorPenetration = 100;
			Vector2 start = base.Projectile.Center;
			Vector2 end = target.Center;
			Color color = (Main.rand.NextBool() ? Color.Magenta : Color.HotPink);
			Vector2 relativePosition = Vector2.Lerp(start, end, 0.5f);
			float scale = 0.015f;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(relativePosition, base.Projectile.SafeDirectionTo(target.Center), "CalamityMod/Particles/BloomLineThick", affectedByGravity: false, 14, scale, color * 0.75f, new Vector2(1f, start.Distance(end) * 0.034f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.25f, 1f, 0.65f));
			targetCount++;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			int dustScale = (int)(10f * base.Projectile.scale);
			int d = Dust.NewDust(base.Projectile.Center - Vector2.One * (float)dustScale, dustScale * 2, dustScale * 2, 242);
			Dust dust = Main.dust[d];
			Vector2 offset = Vector2.Normalize(dust.position - base.Projectile.Center);
			dust.position = base.Projectile.Center + offset * (float)dustScale * base.Projectile.scale;
			if (i < 30)
			{
				dust.velocity = offset * ((Vector2)(ref dust.velocity)).Length();
			}
			else
			{
				dust.velocity = offset * Main.rand.NextFloat(4.5f, 9f);
			}
			dust.color = Main.hslToRgb(0.95f, 0.41f + Main.rand.NextFloat() * 0.2f, 0.93f);
			dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
			dust.noGravity = true;
			dust.scale = 0.7f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		Color drawColor = Color.HotPink;
		Projectile projectile = base.Projectile;
		Color backglowColor = drawColor;
		((Color)(ref backglowColor)).A = 0;
		projectile.DrawProjectileWithBackglow(backglowColor, Color.White, 2.5f * Main.rand.NextFloat(0.8f, 1.3f), null, null, (SpriteEffects)0);
		return false;
	}
}
