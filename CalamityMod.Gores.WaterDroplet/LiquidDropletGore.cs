using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Gores.WaterDroplet;

public abstract class LiquidDropletGore : ModGore
{
	public virtual bool lavaDroplet => false;

	public virtual Vector3 lavaColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Vector3(0f, 0f, 0f);
		}
	}

	public override void SetStaticDefaults()
	{
		ChildSafety.SafeGore[base.Type] = true;
	}

	public override void OnSpawn(Gore gore, IEntitySource source)
	{
		gore.numFrames = 15;
		gore.behindTiles = true;
		gore.timeLeft = Gore.goreTime * 3;
	}

	public override bool Update(Gore gore)
	{
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		gore.alpha = ((!((double)gore.position.Y < Main.worldSurface * 16.0 + 8.0)) ? 100 : 0);
		int frameDuration = 4;
		gore.frameCounter++;
		if (gore.frame <= 4)
		{
			int tileX = (int)(gore.position.X / 16f);
			int tileY = (int)(gore.position.Y / 16f) - 1;
			if (WorldGen.InWorld(tileX, tileY) && !Main.tile[tileX, tileY].HasTile)
			{
				gore.active = false;
			}
			if (gore.frame == 0 || gore.frame == 1 || gore.frame == 2)
			{
				frameDuration = 24 + Main.rand.Next(256);
			}
			if (gore.frame == 3)
			{
				frameDuration = 24 + Main.rand.Next(96);
			}
			if (gore.frameCounter >= frameDuration)
			{
				gore.frameCounter = 0;
				gore.frame++;
				if (gore.frame == 5)
				{
					int droplet = Gore.NewGore(new EntitySource_Misc("0"), gore.position, gore.velocity, gore.type);
					Main.gore[droplet].frame = 9;
					Gore obj = Main.gore[droplet];
					obj.velocity *= 0f;
				}
			}
		}
		else if (gore.frame <= 6)
		{
			frameDuration = 8;
			if (gore.frameCounter >= frameDuration)
			{
				gore.frameCounter = 0;
				gore.frame++;
				if (gore.frame == 7)
				{
					gore.active = false;
				}
			}
		}
		else if (gore.frame <= 9)
		{
			frameDuration = 6;
			gore.velocity.Y += 0.2f;
			if (gore.velocity.Y < 0.5f)
			{
				gore.velocity.Y = 0.5f;
			}
			if (gore.velocity.Y > 12f)
			{
				gore.velocity.Y = 12f;
			}
			if (gore.frameCounter >= frameDuration)
			{
				gore.frameCounter = 0;
				gore.frame++;
			}
			if (gore.frame > 9)
			{
				gore.frame = 7;
			}
		}
		else
		{
			gore.velocity.Y += 0.1f;
			if (gore.frameCounter >= frameDuration)
			{
				gore.frameCounter = 0;
				gore.frame++;
			}
			gore.velocity *= 0f;
			if (gore.frame > 14)
			{
				gore.active = false;
			}
		}
		if (lavaDroplet)
		{
			float num24 = 1f;
			float num25 = 1f;
			float num26 = 1f;
			float num27 = 0.6f;
			float num28 = num27;
			num27 = num28 * gore.frame switch
			{
				0 => 0.1f, 
				1 => 0.2f, 
				2 => 0.3f, 
				3 => 0.4f, 
				4 => 0.5f, 
				5 => 0.4f, 
				6 => 0.2f, 
				7 => 0.5f, 
				8 => 0.5f, 
				9 => 0.5f, 
				10 => 0.5f, 
				11 => 0.4f, 
				12 => 0.3f, 
				13 => 0.2f, 
				14 => 0.1f, 
				_ => 0f, 
			};
			num24 = lavaColor.X / 4f * num27;
			num25 = lavaColor.Y / 4f * num27;
			num26 = lavaColor.Z / 4f * num27;
			Lighting.AddLight(gore.position + new Vector2(8f, 8f), num24, num25, num26);
		}
		Vector2 oldVelocity = gore.velocity;
		gore.velocity = Collision.TileCollision(gore.position, gore.velocity, 16, 14);
		if (gore.velocity != oldVelocity)
		{
			if (gore.frame < 10)
			{
				gore.frame = 10;
				gore.frameCounter = 0;
				if (!lavaDroplet)
				{
					SoundEngine.PlaySound(in SoundID.Drip, gore.position + new Vector2(8f, 8f));
				}
			}
		}
		else if (Collision.WetCollision(gore.position + gore.velocity, 16, 14))
		{
			if (gore.frame < 10)
			{
				gore.frame = 10;
				gore.frameCounter = 0;
				if (!lavaDroplet)
				{
					SoundEngine.PlaySound(in SoundID.Drip, gore.position + new Vector2(8f, 8f));
				}
			}
			int tileX2 = (int)(gore.position.X + 8f) / 16;
			int tileY2 = (int)(gore.position.Y + 14f) / 16;
			if (Main.tile[tileX2, tileY2] != null && Main.tile[tileX2, tileY2].LiquidAmount > 0)
			{
				gore.velocity *= 0f;
				gore.position.Y = tileY2 * 16 - Main.tile[tileX2, tileY2].LiquidAmount / 16;
			}
		}
		gore.position += gore.velocity;
		return false;
	}

	public override Color? GetAlpha(Gore gore, Color lightColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (lavaDroplet)
		{
			return new Color(255, 255, 255, 200);
		}
		return null;
	}
}
