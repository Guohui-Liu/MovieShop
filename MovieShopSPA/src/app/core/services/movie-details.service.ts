import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MovieDetails } from 'src/app/shared/models/moviedetails';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MovieDetailsComponent } from 'src/app/movies/movie-details/movie-details.component';

@Injectable({
  providedIn: 'root'
})
export class MovieDetailsService {

  constructor(private http: HttpClient) {
  }

  getMovieDetails(): Observable<MovieDetails> {
    //  call the API to get the json data
    return this.http.get(`${environment.apiUrl}${'Movies/'}${MovieDetailsComponent}`)
      .pipe(map(resp => resp as MovieDetails))

}
}
