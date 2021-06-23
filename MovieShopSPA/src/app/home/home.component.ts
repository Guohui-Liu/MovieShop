import { Component, OnInit } from '@angular/core';
import { MovieDetailsService } from '../core/services/movie-details.service';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/moviecard';
import { MovieDetails } from '../shared/models/moviedetails';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private movieService: MovieService, private movieDetailsService: MovieDetailsService) { }
movies!:  MovieCard[];
movieDetils!: MovieDetails;
  //born,school.
//angular life cycle hooks
  ngOnInit(): void {

    this.movieService.getTopRevenueMovies().subscribe(
      m =>{
        this.movies = m;
        console.log('inside home component')
        console.log(this.movies);
      }
    );

    
    this.movieDetailsService.getMovieDetails().subscribe(
      (d: MovieDetails) =>{
        this.movieDetils = d;
       // console.log('inside home component')
        //console.log(this.movieDetils);
      }
    );

  }

}
