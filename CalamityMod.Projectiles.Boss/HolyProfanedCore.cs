using System;
using CalamityMod.Items.SummonItems;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyProfanedCore : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 180;

	public const int ShakeThreshold = 90;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName(ModContent.ItemType<ProfanedCore>());

	public override string Texture => "CalamityMod/Items/SummonItems/ProfanedCore";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		int Prov = CalamityGlobalNPC.holyBoss;
		if (Prov == -1)
		{
			base.Projectile.active = false;
			return;
		}
		Timer++;
		if (Timer <= 30f)
		{
			base.Projectile.velocity.Y = -4.5f;
		}
		else
		{
			if (!(Timer > 30f) || !(Timer <= 180f))
			{
				return;
			}
			base.Projectile.velocity = Vector2.Zero;
			Vector2 proviCoreLocation = Main.npc[Prov].Center + new Vector2(0f, 40f);
			Projectile projectile = base.Projectile;
			projectile.Center += (proviCoreLocation - base.Projectile.Center) * 0.0375f;
			if (Timer > 90f)
			{
				Color flameColor = default(Color);
				((Color)(ref flameColor))._002Ector(255, 223, 112);
				Color crystalColor = default(Color);
				((Color)(ref crystalColor))._002Ector(190, 141, 184);
				float starScale = MathHelper.Lerp(0f, 6f, (Timer - 90f) / 90f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(proviCoreLocation, Vector2.Zero, "CalamityMod/Particles/FullStar", affectedByGravity: false, 2, starScale, flameColor, Vector2.One, useAddativeBlend: true, glowCenter: false, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(proviCoreLocation, Vector2.Zero, "CalamityMod/Particles/FullStar", affectedByGravity: false, 2, starScale * 0.4f, flameColor, Vector2.One));
				if (Timer % 2f == 0f)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(proviCoreLocation + Main.rand.NextVector2Circular(300f, 300f), -Vector2.UnitY * 2f, "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 15, 2f, crystalColor, Vector2.One, useAddativeBlend: true, glowCenter: false, 0f, fadeIn: true));
					return;
				}
				Vector2 spawnLocation = proviCoreLocation + Main.rand.NextVector2Circular(250f, 250f);
				GeneralParticleHandler.SpawnParticle(new CustomSprite(spawnLocation, (proviCoreLocation - spawnLocation) * 0.08f, 15, "CalamityMod/NPCs/ProfanedGuardians/ProfanedRocks" + Main.rand.Next(1, 7), 0.3f, Color.White, 0f, AddativeBlend: false)
				{
					Rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f)
				});
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		float shakeAmt = MathHelper.Clamp(MathHelper.Lerp(0f, 8f, (Timer - 90f) / 90f), 0f, 8f);
		Vector2 drawPos = base.Projectile.Center + Main.rand.NextVector2CircularEdge(shakeAmt, shakeAmt);
		Projectile projectile = base.Projectile;
		Color backglowColor = new Color(255, 255, 25);
		Color lightColor2 = lightColor;
		float x = drawPos.X;
		float y = drawPos.Y;
		projectile.DrawProjectileWithBackglow(backglowColor, lightColor2, 3.5f, null, null, (SpriteEffects)0, x, y);
		return false;
	}
}
