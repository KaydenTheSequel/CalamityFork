using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class GaussFlux : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public NPC Target
	{
		get
		{
			return Main.npc[(int)base.Projectile.ai[1]];
		}
		set
		{
			base.Projectile.ai[1] = value.whoAmI;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Lime;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		if (!Target.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Target.Center;
		if (!Main.dedServ)
		{
			if (Time == 0f)
			{
				for (int i = 0; i < 60; i++)
				{
					Vector2 center2 = Target.Center;
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(center2, 261, null, 0, newColor);
					dust.color = Utils.SelectRandom(Main.rand, (Color[])(object)new Color[2]
					{
						Color.Yellow,
						Color.YellowGreen
					});
					dust.velocity = Main.rand.NextVector2Circular(20f, 20f);
					dust.scale = 2f;
					dust.noGravity = true;
				}
			}
			for (int j = 0; j < 7; j++)
			{
				for (int arcIndex = 0; arcIndex < 6; arcIndex++)
				{
					float offsetAngle = MathHelper.ToRadians(1080f) * (float)j / 18f;
					offsetAngle += Time / 10f;
					float scale = 1.4f + (float)Math.Cos((float)j / 7f * ((float)Math.PI * 2f) + Time / 30f) * 0.3f;
					scale *= MathHelper.Lerp(1f, 0.4f, (float)arcIndex / 6f);
					Vector2 offset = Target.Size.RotatedBy(offsetAngle) * 0.5f;
					offset += ((float)arcIndex * ((float)Math.PI * 2f) / 6f + Time / 20f).ToRotationVector2() * 6f * (float)arcIndex;
					Vector2 position = Target.Center + offset;
					newColor = default(Color);
					Dust dust2 = Dust.NewDustPerfect(position, 261, null, 0, newColor);
					dust2.color = Utils.SelectRandom(Main.rand, (Color[])(object)new Color[2]
					{
						Color.Yellow,
						Color.YellowGreen
					});
					dust2.velocity = Vector2.Zero;
					dust2.scale = scale;
					dust2.noGravity = true;
				}
			}
		}
		Time++;
	}
}
