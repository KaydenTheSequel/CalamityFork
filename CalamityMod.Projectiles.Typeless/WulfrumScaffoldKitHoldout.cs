using System;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Tools;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumScaffoldKitHoldout : ModProjectile
{
	public static TemporaryTileManager PipeCleanupManager;

	public static int tileGlowTime = 10;

	public Dictionary<Point, int> SelectedTiles = new Dictionary<Point, int>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<WulfrumScaffoldKit>();

	public Player Owner => Main.player[base.Projectile.owner];

	public WulfrumScaffoldKit Kit => Owner.HeldItem.ModItem as WulfrumScaffoldKit;

	public bool CanOwnerGoOn
	{
		get
		{
			if (Kit.storedScrap <= 0)
			{
				return Owner.HasItem(ModContent.ItemType<WulfrumMetalScrap>());
			}
			return true;
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void Load()
	{
		PipeCleanupManager = new WulfrumPipeManager();
	}

	public bool CanSelectTile(Point tilePos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (SelectedTiles.Count > 0)
		{
			return CanSelectMoreTiles(tilePos);
		}
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				if (Main.tile[tilePos.X + i, tilePos.Y + j].IsTileFull())
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CanSelectMoreTiles(Point tilePos)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = -2; i < 3; i++)
		{
			for (int j = -2; j < 3; j++)
			{
				if ((Math.Abs(i) != 2 || Math.Abs(j) != 2) && (Main.tile[tilePos.X + i, tilePos.Y + j].TileType == WulfrumScaffoldKit.PlacedTileType || SelectedTiles.ContainsKey(new Point(tilePos.X + i, tilePos.Y + j))))
				{
					return true;
				}
			}
		}
		return false;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.netImportant = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.channel || !CanOwnerGoOn)
		{
			return;
		}
		if (base.Projectile.timeLeft > 2)
		{
			base.Projectile.position = Owner.Calamity().mouseWorld;
		}
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		base.Projectile.position = base.Projectile.position.MoveTowards(Owner.Calamity().mouseWorld, 16f);
		Vector2 val = base.Projectile.position - Owner.Center;
		if (((Vector2)(ref val)).Length() > (float)(WulfrumScaffoldKit.TileReach * 16))
		{
			base.Projectile.position = Owner.Center + Vector2.Normalize(base.Projectile.position - Owner.Center) * (float)WulfrumScaffoldKit.TileReach * 16f;
		}
		if (Owner.whoAmI == Main.myPlayer)
		{
			Point tilePos = base.Projectile.position.ToTileCoordinates();
			if (!Main.tile[tilePos].HasTile && !SelectedTiles.ContainsKey(tilePos) && CanSelectTile(tilePos))
			{
				SelectedTiles.Add(tilePos, tileGlowTime);
				if (Kit.storedScrap > 0)
				{
					Kit.storedScrap--;
				}
				else
				{
					Owner.ConsumeItem(ModContent.ItemType<WulfrumMetalScrap>());
					Kit.storedScrap = WulfrumScaffoldKit.TilesPerScrap - 1;
					SoundEngine.PlaySound(in SoundID.Item65);
					if (!Main.dedServ)
					{
						Gore gore = Gore.NewGoreDirect(base.Projectile.GetSource_FromThis(), Owner.Center, Main.rand.NextVector2Circular(4f, 4f), base.Mod.Find<ModGore>("WulfrumPinger2").Type, Main.rand.NextFloat(0.5f, 1f));
						gore.timeLeft = 10;
						gore.alpha = 100 - Main.rand.Next(0, 60);
					}
				}
			}
			foreach (Point position in SelectedTiles.Keys)
			{
				if (SelectedTiles[position] > 0)
				{
					SelectedTiles[position]--;
				}
			}
		}
		base.Projectile.timeLeft = 2;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != Owner.whoAmI)
		{
			return false;
		}
		Texture2D sprite = TextureAssets.Projectile[base.Type].Value;
		Effect tileEffect = Filters.Scene["CalamityMod:WulfrumScaffoldSelection"].GetShader().Shader;
		tileEffect.Parameters["mainOpacity"].SetValue(1f);
		tileEffect.Parameters["tileEdgeBlendStrenght"].SetValue(2f);
		EffectParameter obj = tileEffect.Parameters["placementGlowColor"];
		Color val = Color.GreenYellow;
		obj.SetValue(((Color)(ref val)).ToVector4());
		EffectParameter obj2 = tileEffect.Parameters["baseTintColor"];
		val = Color.DeepSkyBlue;
		obj2.SetValue(((Color)(ref val)).ToVector4() * 0.5f);
		EffectParameter obj3 = tileEffect.Parameters["scanlineColor"];
		val = Color.YellowGreen;
		obj3.SetValue(((Color)(ref val)).ToVector4() * 1f);
		EffectParameter obj4 = tileEffect.Parameters["tileEdgeColor"];
		val = Color.GreenYellow;
		obj4.SetValue(((Color)(ref val)).ToVector3());
		tileEffect.Parameters["Resolution"].SetValue(8f);
		tileEffect.Parameters["time"].SetValue((float)Main.GameUpdateCount);
		Vector4[] scanLines = (Vector4[])(object)new Vector4[7]
		{
			new Vector4(0f, 4f, 0.1f, 0.5f),
			new Vector4(1f, 4f, 0.1f, 0.5f),
			new Vector4(37f, 60f, 0.4f, 1f),
			new Vector4(2f, 6f, -0.2f, 0.3f),
			new Vector4(0f, 4f, 0.1f, 0.5f),
			new Vector4(1f, 4f, 0.1f, 0.5f),
			new Vector4(2f, 6f, -0.2f, 0.3f)
		};
		tileEffect.Parameters["ScanLines"].SetValue(scanLines);
		tileEffect.Parameters["ScanLinesCount"].SetValue(scanLines.Length);
		tileEffect.Parameters["verticalScanLinesIndex"].SetValue(4);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, tileEffect, Main.GameViewMatrix.TransformationMatrix);
		foreach (Point pos in SelectedTiles.Keys)
		{
			tileEffect.Parameters["blinkTime"].SetValue((float)SelectedTiles[pos] / (float)tileGlowTime);
			tileEffect.Parameters["cardinalConnections"].SetValue(new bool[4]
			{
				Connected(pos, 0, -1),
				Connected(pos, -1, 0),
				Connected(pos, 1, 0),
				Connected(pos, 0, 1)
			});
			tileEffect.Parameters["tilePosition"].SetValue(pos.ToVector2() * 16f);
			Main.spriteBatch.Draw(sprite, pos.ToWorldCoordinates() - Main.screenPosition, (Rectangle?)null, Color.White, 0f, new Vector2((float)sprite.Width / 2f, (float)sprite.Height / 2f), 16f, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public bool Connected(Point pos, int displaceX, int displaceY)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		return SelectedTiles.ContainsKey(new Point(pos.X + displaceX, pos.Y + displaceY));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		if (SelectedTiles.Keys.Count > 0)
		{
			SoundStyle style = SoundID.Item101 with
			{
				Volume = SoundID.Item101.Volume * 0.6f
			};
			SoundEngine.PlaySound(in style, Owner.Center);
		}
		if (Main.myPlayer != Owner.whoAmI)
		{
			return;
		}
		foreach (Point pos in SelectedTiles.Keys)
		{
			if (PipeCleanupManager == null)
			{
				PipeCleanupManager = new WulfrumPipeManager();
			}
			TempTilesManagerSystem.AddTemporaryTile(pos, PipeCleanupManager);
			WorldGen.PlaceTile(pos.X, pos.Y, WulfrumScaffoldKit.PlacedTileType);
			NetMessage.SendTileSquare(-1, pos.X, pos.Y);
		}
	}
}
