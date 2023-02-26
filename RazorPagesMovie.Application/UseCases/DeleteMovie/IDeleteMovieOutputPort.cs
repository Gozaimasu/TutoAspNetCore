﻿using RazorPagesMovie.Domain;

namespace RazorPagesMovie.Application.UseCases.DeleteMovie;

public interface IDeleteMovieOutputPort
{
    void Ok();
    void NotFound();
}
