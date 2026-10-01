using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CalamityMod.Enums;
using CalamityMod.Systems.Graphic;
using CalamityMod.Systems.Graphic.PixelationSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Particles;

[Autoload(true, Side = ModSide.Client)]
public sealed class GeneralParticleHandler : ModSystem
{
	internal static Dictionary<Type, int> particleIDsByTypes;

	internal static Dictionary<int, Asset<Texture2D>> particleTexturesByIDs;

	private static List<Particle> activeParticles;

	private static List<Particle> particlesToKill;

	private static Dictionary<GeneralDrawLayer, Queue<Particle>> particlesToSpawnNextFrame;

	private static Dictionary<GeneralDrawLayer, Queue<Particle>> particlesToSpawnNextFrame_Pixelated;

	private static Dictionary<BlendState, List<Particle>> particlesToDraw;

	private static Dictionary<BlendState, List<Particle>> particlesToDraw_Pixelated;

	private static Dictionary<Effect, Dictionary<BlendState, List<Particle>>> particlesToDraw_CustomShader;

	public override void PostSetupContent()
	{
		ReflectionHelper.IterateEveryModsTypes<Particle>(delegate(Type type)
		{
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			int count = particleIDsByTypes.Count;
			particleIDsByTypes[type] = count;
			Particle particle = (Particle)RuntimeHelpers.GetUninitializedObject(type);
			string name = type.Namespace.Replace('.', '/') + "/" + type.Name;
			if (particle.Texture != "")
			{
				name = particle.Texture;
			}
			particleTexturesByIDs[count] = ModContent.Request<Texture2D>(name, particle.TextureRequestMode);
		});
	}

	public override void Load()
	{
		particleIDsByTypes = new Dictionary<Type, int>();
		particleTexturesByIDs = new Dictionary<int, Asset<Texture2D>>();
		activeParticles = new List<Particle>();
		particlesToKill = new List<Particle>();
		particlesToSpawnNextFrame = new Dictionary<GeneralDrawLayer, Queue<Particle>>();
		particlesToSpawnNextFrame_Pixelated = new Dictionary<GeneralDrawLayer, Queue<Particle>>();
		particlesToDraw = new Dictionary<BlendState, List<Particle>>();
		particlesToDraw_Pixelated = new Dictionary<BlendState, List<Particle>>();
		particlesToDraw_CustomShader = new Dictionary<Effect, Dictionary<BlendState, List<Particle>>>();
		GeneralDrawLayerSystem.OnDrawLayer += DrawParticleCollectionsAtSpecificLayer;
	}

	public override void Unload()
	{
		particleIDsByTypes = null;
		particleTexturesByIDs = null;
		activeParticles = null;
		particlesToKill = null;
		particlesToSpawnNextFrame = null;
		particlesToSpawnNextFrame_Pixelated = null;
		particlesToDraw = null;
		particlesToDraw_Pixelated = null;
		particlesToDraw_CustomShader = null;
	}

	public override void OnWorldUnload()
	{
		activeParticles.Clear();
		particlesToKill.Clear();
		particlesToSpawnNextFrame.Clear();
		particlesToSpawnNextFrame_Pixelated.Clear();
		particlesToDraw.Clear();
		particlesToDraw_Pixelated.Clear();
		particlesToDraw_CustomShader.Clear();
	}

	public static void SpawnParticle(Particle particle, bool pixelate = false, GeneralDrawLayer? manualDrawLayerOverride = null)
	{
		if (!Main.gamePaused && !Main.dedServ && activeParticles != null && (activeParticles.Count < CalamityClientConfig.Instance.ParticleLimit || particle.Important))
		{
			particle.Pixelate = pixelate;
			if (manualDrawLayerOverride.HasValue)
			{
				particle.DrawLayer = manualDrawLayerOverride.Value;
			}
			activeParticles.Add(particle);
			ReturnAssociatedDrawCollection(particle).Add(particle);
			particle.Type = particleIDsByTypes[particle.GetType()];
		}
	}

	public static void QueueParticleForNextFrame(Particle particle, bool pixelate = false, GeneralDrawLayer? manualDrawLayerOverride = null)
	{
		if (Main.gamePaused || Main.dedServ || activeParticles == null)
		{
			return;
		}
		GeneralDrawLayer actualDrawLayer = manualDrawLayerOverride ?? particle.DrawLayer;
		if (pixelate)
		{
			if (!particlesToSpawnNextFrame_Pixelated.ContainsKey(actualDrawLayer))
			{
				particlesToSpawnNextFrame_Pixelated[actualDrawLayer] = new Queue<Particle>();
			}
			particlesToSpawnNextFrame_Pixelated[actualDrawLayer].Enqueue(particle);
		}
		else
		{
			if (!particlesToSpawnNextFrame.ContainsKey(actualDrawLayer))
			{
				particlesToSpawnNextFrame[actualDrawLayer] = new Queue<Particle>();
			}
			particlesToSpawnNextFrame[actualDrawLayer].Enqueue(particle);
		}
	}

	public static void RemoveParticle(Particle particle)
	{
		if (!Main.dedServ)
		{
			particlesToKill.Add(particle);
		}
	}

	public static void RemoveParticlesOfType<T>() where T : Particle
	{
		if (Main.dedServ || activeParticles == null || activeParticles.Count == 0)
		{
			return;
		}
		foreach (Particle particle in activeParticles)
		{
			if (particle.GetType() == typeof(T))
			{
				particlesToKill.Add(particle);
			}
		}
	}

	public static void RemoveAllParticles()
	{
		if (Main.dedServ || activeParticles == null || activeParticles.Count == 0)
		{
			return;
		}
		foreach (Particle particle in activeParticles)
		{
			particlesToKill.Add(particle);
		}
	}

	public static int FreeSpacesAvailable()
	{
		if (Main.dedServ || activeParticles == null)
		{
			return 0;
		}
		return CalamityClientConfig.Instance.ParticleLimit - activeParticles.Count();
	}

	public static Texture2D GetTexture(int type)
	{
		if (Main.dedServ)
		{
			return null;
		}
		return particleTexturesByIDs[type].Value;
	}

	public static void Update()
	{
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		foreach (KeyValuePair<GeneralDrawLayer, Queue<Particle>> collectionsByDrawLayer in particlesToSpawnNextFrame)
		{
			while (collectionsByDrawLayer.Value.Count > 0)
			{
				SpawnParticle(collectionsByDrawLayer.Value.Dequeue(), pixelate: false, collectionsByDrawLayer.Key);
			}
		}
		foreach (KeyValuePair<GeneralDrawLayer, Queue<Particle>> collectionsByDrawLayer2 in particlesToSpawnNextFrame_Pixelated)
		{
			while (collectionsByDrawLayer2.Value.Count > 0)
			{
				SpawnParticle(collectionsByDrawLayer2.Value.Dequeue(), pixelate: true, collectionsByDrawLayer2.Key);
			}
		}
		foreach (Particle particle in activeParticles)
		{
			if (particle != null)
			{
				particle.Position += particle.Velocity;
				particle.Time++;
				particle.Update();
			}
		}
		activeParticles.RemoveAll(delegate(Particle particle2)
		{
			if ((particle2.Time >= particle2.Lifetime && particle2.SetLifetime) || particlesToKill.Contains(particle2))
			{
				ReturnAssociatedDrawCollection(particle2).Remove(particle2);
				return true;
			}
			return false;
		});
		particlesToKill.Clear();
	}

	private static void DrawParticleCollectionsAtSpecificLayer(GeneralDrawLayer drawLayer)
	{
		if (!Main.dedServ)
		{
			DrawParticleCollection(particlesToDraw, drawLayer);
			DrawParticleCollection(particlesToDraw_Pixelated, drawLayer, pixelated: true);
			DrawParticlesWithShaders(drawLayer);
		}
	}

	private static void DrawParticleInstance(Particle particle)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		int drawIterations = ((!Main.LocalPlayer.Calamity().trippy) ? 1 : 4);
		for (int i = 0; i < drawIterations; i++)
		{
			Vector2 positionSpoof = particle.Position;
			Vector2 positionDiff = positionSpoof - Main.LocalPlayer.Center;
			switch (i)
			{
			case 1:
				particle.Position = Main.LocalPlayer.Center - positionDiff;
				break;
			case 2:
				particle.Position = Main.LocalPlayer.Center - Vector2.UnitY * positionDiff.Y + Vector2.UnitX * positionDiff.X;
				break;
			case 3:
				particle.Position = Main.LocalPlayer.Center - Vector2.UnitX * positionDiff.X + Vector2.UnitY * positionDiff.Y;
				break;
			}
			if (Main.LocalPlayer.Calamity().trippy)
			{
				particle.Color = Main.DiscoColor;
			}
			if (particle.UseCustomDraw)
			{
				particle.CustomDraw(Main.spriteBatch);
			}
			else
			{
				Color lightColor = particle.Color;
				if (particle.AffectedByLight)
				{
					lightColor = particle.Color.MultiplyRGB(Lighting.GetColor((particle.Position / 16f).ToPoint()));
				}
				Rectangle frame = particleTexturesByIDs[particle.Type].Frame(1, particle.FrameVariants, 0, particle.Variant);
				Main.spriteBatch.Draw(particleTexturesByIDs[particle.Type].Value, particle.Position - Main.screenPosition, (Rectangle?)frame, lightColor, particle.Rotation, frame.Size() * 0.5f, particle.Scale, (SpriteEffects)0, 0f);
			}
			particle.Position = positionSpoof;
		}
	}

	private static void DrawParticleCollection(Dictionary<BlendState, List<Particle>> drawCollection, GeneralDrawLayer drawLayer, bool pixelated = false)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		RasterizerState scissorRectRasterizer = Main.Rasterizer;
		scissorRectRasterizer.ScissorTestEnable = true;
		Main.graphics.GraphicsDevice.RasterizerState.ScissorTestEnable = true;
		Main.graphics.GraphicsDevice.ScissorRectangle = new Rectangle(0, 0, Main.screenWidth, Main.screenHeight);
		foreach (KeyValuePair<BlendState, List<Particle>> keyValuePair in drawCollection)
		{
			if (pixelated)
			{
				PixelationManager.AddPixelatedDrawer(delegate
				{
					IEnumerable<Particle> enumerable = keyValuePair.Value.Where((Particle p) => p.DrawLayer == drawLayer);
					if (enumerable.Any())
					{
						foreach (Particle item in enumerable)
						{
							DrawParticleInstance(item);
						}
					}
				}, drawLayer, keyValuePair.Key);
				continue;
			}
			Main.spriteBatch.Begin((SpriteSortMode)0, keyValuePair.Key, SamplerState.LinearClamp, DepthStencilState.None, scissorRectRasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			IEnumerable<Particle> particlesAtSpecifiedLayer = keyValuePair.Value.Where((Particle p) => p.DrawLayer == drawLayer);
			if (particlesAtSpecifiedLayer.Any())
			{
				foreach (Particle item2 in particlesAtSpecifiedLayer)
				{
					DrawParticleInstance(item2);
				}
			}
			Main.spriteBatch.End();
		}
	}

	private static void DrawParticlesWithShaders(GeneralDrawLayer drawLayer)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		foreach (KeyValuePair<Effect, Dictionary<BlendState, List<Particle>>> shaderDrawCollectionPair in particlesToDraw_CustomShader)
		{
			foreach (KeyValuePair<BlendState, List<Particle>> blendStateParticleListPair in shaderDrawCollectionPair.Value)
			{
				if (blendStateParticleListPair.Value.Count == 0)
				{
					return;
				}
				Main.spriteBatch.Begin((SpriteSortMode)1, blendStateParticleListPair.Key, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, shaderDrawCollectionPair.Key, Main.GameViewMatrix.TransformationMatrix);
				foreach (Particle item in blendStateParticleListPair.Value.Where((Particle p) => p.DrawLayer == drawLayer))
				{
					item.PrepareCustomShader(shaderDrawCollectionPair.Key);
					DrawParticleInstance(item);
				}
				Main.spriteBatch.End();
			}
		}
	}

	private static List<Particle> ReturnAssociatedDrawCollection(Particle particle)
	{
		if (particle.CustomShader != null)
		{
			if (!particlesToDraw_CustomShader.ContainsKey(particle.CustomShader))
			{
				particlesToDraw_CustomShader[particle.CustomShader] = new Dictionary<BlendState, List<Particle>>();
			}
			if (particle.UseAdditiveBlend)
			{
				if (!particlesToDraw_CustomShader[particle.CustomShader].ContainsKey(BlendState.Additive))
				{
					particlesToDraw_CustomShader[particle.CustomShader][BlendState.Additive] = new List<Particle>();
				}
				return particlesToDraw_CustomShader[particle.CustomShader][BlendState.Additive];
			}
			if (particle.UseHalfTransparency)
			{
				if (!particlesToDraw_CustomShader[particle.CustomShader].ContainsKey(BlendState.NonPremultiplied))
				{
					particlesToDraw_CustomShader[particle.CustomShader][BlendState.NonPremultiplied] = new List<Particle>();
				}
				return particlesToDraw_CustomShader[particle.CustomShader][BlendState.NonPremultiplied];
			}
			if (!particlesToDraw_CustomShader[particle.CustomShader].ContainsKey(BlendState.AlphaBlend))
			{
				particlesToDraw_CustomShader[particle.CustomShader][BlendState.AlphaBlend] = new List<Particle>();
			}
			return particlesToDraw_CustomShader[particle.CustomShader][BlendState.AlphaBlend];
		}
		if (particle.Pixelate)
		{
			if (particle.UseAdditiveBlend)
			{
				if (!particlesToDraw_Pixelated.ContainsKey(BlendState.Additive))
				{
					particlesToDraw_Pixelated[BlendState.Additive] = new List<Particle>();
				}
				return particlesToDraw_Pixelated[BlendState.Additive];
			}
			if (particle.UseHalfTransparency)
			{
				if (!particlesToDraw_Pixelated.ContainsKey(BlendState.NonPremultiplied))
				{
					particlesToDraw_Pixelated[BlendState.NonPremultiplied] = new List<Particle>();
				}
				return particlesToDraw_Pixelated[BlendState.NonPremultiplied];
			}
			if (!particlesToDraw_Pixelated.ContainsKey(BlendState.AlphaBlend))
			{
				particlesToDraw_Pixelated[BlendState.AlphaBlend] = new List<Particle>();
			}
			return particlesToDraw_Pixelated[BlendState.AlphaBlend];
		}
		if (particle.UseAdditiveBlend)
		{
			if (!particlesToDraw.ContainsKey(BlendState.Additive))
			{
				particlesToDraw[BlendState.Additive] = new List<Particle>();
			}
			return particlesToDraw[BlendState.Additive];
		}
		if (particle.UseHalfTransparency)
		{
			if (!particlesToDraw.ContainsKey(BlendState.NonPremultiplied))
			{
				particlesToDraw[BlendState.NonPremultiplied] = new List<Particle>();
			}
			return particlesToDraw[BlendState.NonPremultiplied];
		}
		if (!particlesToDraw.ContainsKey(BlendState.AlphaBlend))
		{
			particlesToDraw[BlendState.AlphaBlend] = new List<Particle>();
		}
		return particlesToDraw[BlendState.AlphaBlend];
	}
}
