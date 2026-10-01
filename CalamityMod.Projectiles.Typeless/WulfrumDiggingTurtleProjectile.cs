using System;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumDiggingTurtleProjectile : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle IdleSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumSawIdle")
	{
		IsLooped = true,
		Volume = 0.8f,
		MaxInstances = 0
	};

	public static readonly SoundStyle CuttingSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumSawCutting")
	{
		IsLooped = true,
		Volume = 0.7f,
		MaxInstances = 0
	};

	public static readonly SoundStyle BreakingSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumMachineBreak");

	private SlotId CuttingSoundSlot;

	private SlotId IdlingSoundSlot;

	public static Texture2D SmallGearTexture;

	public static Texture2D GearTexture;

	public static int Lifetime = 400;

	public static int DigTime = 350;

	public static float DigSpeed = 1.5f;

	public static int MaxPickPower = 1000;

	public static float ClearSpaceDiagonal = 50f;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public bool Diggging
	{
		get
		{
			return base.Projectile.ai[0] == 1f;
		}
		set
		{
			base.Projectile.ai[0] = (value ? 1f : 0f);
		}
	}

	public bool HasDug
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = (value ? 1f : 0f);
		}
	}

	public float CuttingVolume
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = Math.Clamp(value, 0f, 1f);
		}
	}

	public override string Texture => "CalamityMod/Items/Tools/WulfrumDiggingTurtle";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.netImportant = true;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (!HasDug)
		{
			HasDug = true;
			base.Projectile.timeLeft = DigTime;
		}
		Diggging = true;
		base.Projectile.velocity = oldVelocity.SafeNormalize(Vector2.UnitY) * DigSpeed;
		return false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color greenYellow = Color.GreenYellow;
		Lighting.AddLight(center, ((Color)(ref greenYellow)).ToVector3() * 0.75f);
		if (!SoundEngine.TryGetActiveSound(IdlingSoundSlot, out ActiveSound idleSoundOut))
		{
			SoundStyle style = IdleSound with
			{
				Volume = IdleSound.Volume
			};
			IdlingSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else if (idleSoundOut != null)
		{
			idleSoundOut.Volume = 1f - CuttingVolume;
			idleSoundOut.Position = base.Projectile.Center;
		}
		if (!SoundEngine.TryGetActiveSound(CuttingSoundSlot, out ActiveSound cuttingSoundOut))
		{
			SoundStyle style = CuttingSound with
			{
				Volume = CuttingSound.Volume
			};
			CuttingSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		else if (cuttingSoundOut != null)
		{
			cuttingSoundOut.Volume = CuttingVolume;
			cuttingSoundOut.Position = base.Projectile.Center;
		}
		if (Diggging)
		{
			CuttingVolume += 0.1f;
			for (int i = -1; i <= 1; i++)
			{
				Point tilePos = (base.Projectile.Center + (base.Projectile.rotation + (float)Math.PI / 4f * (float)i).ToRotationVector2() * 16f).ToTileCoordinates();
				DigTile(tilePos.X, tilePos.Y);
			}
		}
		else
		{
			CuttingVolume -= 0.1f;
			float fallSpeed = base.Projectile.velocity.Y;
			if (base.Projectile.timeLeft < 345)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += Vector2.UnitY * 0.5f * (1f - Math.Clamp(((float)base.Projectile.timeLeft - 310f) / 35f, 0f, 1f));
			}
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.98f;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = Math.Clamp(base.Projectile.velocity.Y, 0f, Math.Max(18f, fallSpeed));
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		if (!Collision.SolidCollision(base.Projectile.Center - Vector2.One * ClearSpaceDiagonal * 0.5f, (int)ClearSpaceDiagonal, (int)ClearSpaceDiagonal))
		{
			Diggging = false;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI && Main.rand.NextBool() && !base.Projectile.noDropItem)
		{
			Item.NewItem(base.Projectile.GetSource_DropAsItem(), (int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height, ModContent.ItemType<WulfrumMetalScrap>());
		}
		if (SoundEngine.TryGetActiveSound(CuttingSoundSlot, out ActiveSound cuttingSoundOut))
		{
			cuttingSoundOut.Stop();
		}
		if (SoundEngine.TryGetActiveSound(IdlingSoundSlot, out ActiveSound idleSoundOut))
		{
			idleSoundOut.Stop();
		}
		SoundEngine.PlaySound(in BreakingSound, base.Projectile.position);
		int smokeCount = Main.rand.Next(5, 10);
		int sparkCount = Main.rand.Next(4, 8);
		Color smokeEnd = default(Color);
		for (int i = 0; i < smokeCount; i++)
		{
			Vector2 velocity = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(3f, 12f);
			Color smokeStart = (Main.rand.NextBool() ? Color.GreenYellow : Color.Aqua);
			((Color)(ref smokeEnd))._002Ector(60, 60, 60);
			float smokeSize = Main.rand.NextFloat(1.4f, 2.2f);
			GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(base.Projectile.Center, velocity, smokeStart, smokeEnd, smokeSize, 135 - Main.rand.Next(30)));
		}
		if (!Main.dedServ)
		{
			for (int j = 0; j < 4; j++)
			{
				Vector2 shrapnelVelocity = Main.rand.NextVector2Circular(9f, 9f);
				float shrapnelScale = Main.rand.NextFloat(0.8f, 1f);
				string goreType = ((j < 2) ? "WulfrumTurtle1" : ((j < 3) ? "WulfrumTurtle2" : "WulfrumTurtle3"));
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, shrapnelVelocity, base.Mod.Find<ModGore>(goreType).Type, shrapnelScale);
			}
		}
		for (int k = 0; k < sparkCount; k++)
		{
			Vector2 center = base.Projectile.Center;
			Vector2? velocity2 = Main.rand.NextVector2Circular(18f, 18f);
			float scale = Main.rand.NextFloat(0.4f, 1f);
			Dust.NewDustPerfect(center, 226, velocity2, 0, default(Color), scale);
		}
	}

	public void DigTile(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (!tile.HasTile)
		{
			return;
		}
		int pickPower = Math.Min(Owner.GetBestPickPower(), MaxPickPower);
		int pickaxeRequirement = tile.GetRequiredPickPower(x, y);
		bool true_ = true;
		bool false_ = false;
		bool canBreakTileCheck = TileLoader.CanKillTile(x, y, tile.TileType, ref true_) && TileLoader.CanKillTile(x, y, tile.TileType, ref false_);
		bool shouldBreakTile = tile.ShouldBeMined();
		if (((!Owner.noBuilding & shouldBreakTile) && pickaxeRequirement <= pickPower) & canBreakTileCheck)
		{
			WorldGen.KillTile(x, y);
			if (!Main.tile[x, y].HasTile && Main.netMode != 0)
			{
				NetMessage.SendData(17, -1, -1, null, 0, x, y);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		GearTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/WulfrumDiggingTurtle_Gear", (AssetRequestMode)2).Value;
		SmallGearTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/WulfrumDiggingTurtle_SmallGear", (AssetRequestMode)2).Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		if (Diggging)
		{
			position += Main.rand.NextVector2Circular(2f, 2f);
		}
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 2f;
		for (int i = -1; i <= 1; i += 2)
		{
			Vector2 diggingGearOffset = Utils.RotatedBy(new Vector2((float)(9 * i), -11f), (double)drawRotation, default(Vector2)) * base.Projectile.scale;
			Main.EntitySpriteDraw(SmallGearTexture, position + diggingGearOffset, null, lightColor, drawRotation + Main.GlobalTimeWrappedHourly * -10f * (float)i, SmallGearTexture.Size() / 2f, base.Projectile.scale * 1.2f, (SpriteEffects)0);
		}
		Vector2 largeGearOffset = Utils.RotatedBy(new Vector2(0f, 3f), (double)drawRotation, default(Vector2)) * base.Projectile.scale;
		Main.EntitySpriteDraw(GearTexture, position + largeGearOffset, null, lightColor, Main.GlobalTimeWrappedHourly * 6f, GearTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, position, null, lightColor, drawRotation, texture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
