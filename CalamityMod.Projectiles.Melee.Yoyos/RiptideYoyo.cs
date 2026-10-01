using System.IO;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class RiptideYoyo : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Riptide>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = Riptide.Duration;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = Riptide.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = Riptide.Speed;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 22);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref val)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] % 15f != 0f)
		{
			return;
		}
		float xVelocity = 0f;
		float yVelocity = -10f;
		float xIncrement = 1.2f;
		float yIncrement = 0.2f;
		float num = base.Projectile.localAI[1] / 15f;
		if (num != 1f)
		{
			if (num != 2f)
			{
				if (num != 3f)
				{
					if (num != 4f)
					{
						if (num != 5f)
						{
							if (num != 6f)
							{
								if (num != 7f)
								{
									if (num == 8f)
									{
										base.Projectile.localAI[1] = 0f;
										xVelocity = -10f;
										yVelocity = -10f;
										xIncrement = 0.7f;
										yIncrement = -0.7f;
									}
								}
								else
								{
									xVelocity = -10f;
									yVelocity = 0f;
									xIncrement = -0.2f;
									yIncrement = -1.2f;
								}
							}
							else
							{
								xVelocity = -5f;
								yVelocity = 5f;
								xIncrement = -0.7f;
								yIncrement = -0.7f;
							}
						}
						else
						{
							xVelocity = 0f;
							yVelocity = 10f;
							xIncrement = -1.2f;
							yIncrement = -0.2f;
						}
					}
					else
					{
						xVelocity = 5f;
						yVelocity = 5f;
						xIncrement = -0.7f;
						yIncrement = 0.7f;
					}
				}
				else
				{
					xVelocity = 10f;
					yVelocity = 0f;
					xIncrement = 0.2f;
					yIncrement = 1.2f;
				}
			}
			else
			{
				xVelocity = 5f;
				yVelocity = -5f;
				xIncrement = 0.7f;
				yIncrement = 0.7f;
			}
		}
		SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.position);
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, new Vector2(xVelocity, yVelocity), ModContent.ProjectileType<AquaStream>(), base.Projectile.damage, 0f, base.Projectile.owner, xIncrement, yIncrement);
	}
}
