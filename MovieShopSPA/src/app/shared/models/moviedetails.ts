import { Cast } from "./cast";
import { Genre } from "./genre";

export interface MovieDetails{
    id: number;
        title: string;
        posterUrl: string;
        backdropUrl: string;
        rating: number;
        overview: string;
        tagline: string;
        budget: number;
        revenue: number;
        imdbUrl: string;
        tmdbUrl: string;
        runTime: number;
        price: number;
        releaseDate: Date;
        favoritesCount: number;
        casts: Cast[];
        genres: Genre[];
}