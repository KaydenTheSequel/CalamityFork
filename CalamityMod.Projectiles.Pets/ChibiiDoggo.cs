using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class ChibiiDoggo : ModProjectile, ILocalizedModType, IModType
{
	public int trueType;

	public bool previousCollide;

	public bool yFlip;

	public float notlocalai1;

	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 11;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, Main.projFrames[base.Type], 6).WithOffset(-7f, 0f).WithSpriteDirection(-1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 38;
		base.Projectile.height = 46;
		base.Projectile.aiStyle = 26;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.scale = 0.8f;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		trueType = base.Projectile.type;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Color colorArea = Lighting.GetColor((int)(base.Projectile.Center.X / 16f), (int)(base.Projectile.Center.Y / 16f));
		Texture2D texture2D3 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Pets/ChibiiDoggoMonochrome", (AssetRequestMode)2).Value;
		int textureArea = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y3 = textureArea * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, y3, texture2D3.Width, textureArea);
		Vector2 halfRect = rectangle.Size() / 2f;
		int eightCompare = 8;
		int twoConst = 2;
		for (int counter = 1; (twoConst > 0 && counter < eightCompare) || (twoConst < 0 && counter > eightCompare); counter += twoConst)
		{
			Color colorAlpha = colorArea;
			colorAlpha = base.Projectile.GetAlpha(colorAlpha);
			float trailColorChange = eightCompare - counter;
			if (twoConst < 0)
			{
				trailColorChange = 1 - counter;
			}
			colorAlpha *= trailColorChange / ((float)ProjectileID.Sets.TrailCacheLength[base.Type] * 1.5f);
			Vector2 oldDrawPos = base.Projectile.oldPos[counter];
			float projRotate = base.Projectile.rotation;
			SpriteEffects effects = spriteEffects;
			Main.spriteBatch.Draw(texture2D3, oldDrawPos + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, colorAlpha, projRotate + base.Projectile.rotation * 0f * (float)(counter - 1) * (float)base.Projectile.spriteDirection, halfRect, base.Projectile.scale, effects, 0f);
		}
		Main.spriteBatch.Draw(TextureAssets.Projectile[base.Type].Value, base.Projectile.position + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)rectangle, lightColor, base.Projectile.rotation, halfRect, base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override bool PreAI()
	{
		base.Projectile.type = 319;
		Main.player[base.Projectile.owner].blackCat = false;
		return true;
	}

	public void PreventFastfall()
	{
		yFlip = !yFlip;
		if (yFlip && base.Projectile.velocity.Y > 0f)
		{
			base.Projectile.position.Y -= base.Projectile.velocity.Y;
		}
	}

	public override void AI()
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.type = trueType;
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.chibii = false;
		}
		if (modPlayer.chibii)
		{
			base.Projectile.timeLeft = 2;
		}
		if (previousCollide != base.Projectile.tileCollide)
		{
			previousCollide = base.Projectile.tileCollide;
			for (int i = 0; i < 10; i++)
			{
				base.Projectile.oldPos[i] = base.Projectile.position;
			}
			for (int j = 0; j < 77; j++)
			{
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 182, base.Projectile.velocity.X * 0.7f, base.Projectile.velocity.Y * 0.7f, 100, default(Color), 2.5f);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 1.5f;
				Main.dust[dust].noLight = true;
				switch (Main.rand.Next(3))
				{
				case 0:
				{
					Dust obj3 = Main.dust[dust];
					obj3.velocity *= 1.5f;
					Main.dust[dust].scale *= 0.7f;
					break;
				}
				case 1:
				{
					Dust obj2 = Main.dust[dust];
					obj2.velocity *= 1.2f;
					Main.dust[dust].scale *= 0.9f;
					break;
				}
				}
			}
		}
		if (base.Projectile.tileCollide)
		{
			base.Projectile.hide = false;
			PreventFastfall();
		}
		else
		{
			base.Projectile.hide = true;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ChibiiDoggoFly>()] <= 0)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, 0f, 0f, ModContent.ProjectileType<ChibiiDoggoFly>(), 0, 0f, base.Projectile.owner, base.Projectile.identity);
			}
		}
		if (Main.dedServ)
		{
			return;
		}
		Color color = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16);
		Vector3 vector3_1 = ((Color)(ref color)).ToVector3();
		color = Lighting.GetColor((int)player.Center.X / 16, (int)player.Center.Y / 16);
		Vector3 vector3_2 = ((Color)(ref color)).ToVector3();
		if ((double)((Vector3)(ref vector3_1)).Length() < 0.150000005960464 && (double)((Vector3)(ref vector3_2)).Length() < 0.150000005960464)
		{
			notlocalai1++;
		}
		else if ((double)notlocalai1 > 0.0)
		{
			notlocalai1--;
		}
		notlocalai1 = MathHelper.Clamp(notlocalai1, -3600f, 120f);
		if (!((double)notlocalai1 > (double)Main.rand.Next(30, 120)) || player.immune || player.velocity.X != 0f || player.velocity.Y != 0f)
		{
			return;
		}
		if (Main.rand.NextBool(3))
		{
			if (Main.rand.NextBool())
			{
				SoundStyle style = SoundID.Meowmere with
				{
					Volume = SoundID.Meowmere.Volume * 4f
				};
				SoundEngine.PlaySound(in style, base.Projectile.position);
			}
			else
			{
				SoundStyle style = SoundID.ScaryScream with
				{
					Volume = SoundID.ScaryScream.Volume * 2f
				};
				SoundEngine.PlaySound(in style, player.position);
			}
			notlocalai1 = -600f;
		}
		else
		{
			notlocalai1 = Main.rand.Next(30) * -10 - 300;
			SoundEngine.PlaySound(in SoundID.Item1, base.Projectile.Center);
			if (Main.rand.NextBool())
			{
				player.Hurt(PlayerDeathReason.ByOther(6), 500, 0);
			}
			else
			{
				player.Hurt(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ChibiiDoggo").ToNetworkText(player.name)), 500, 0);
			}
			player.RemoveAllIFrames();
		}
		base.Projectile.netUpdate = true;
	}
}
