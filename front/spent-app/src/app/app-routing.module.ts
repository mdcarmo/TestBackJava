import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SigninComponent } from './components/signin/signin.component';
import { SpentDetailComponent } from './components/spent-detail/spent-detail.component';
import { SpentsComponent } from './components/spents/spents.component';
import { AuthGuard } from './shared/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/log-in', pathMatch: 'full' },
  { path: 'log-in', component: SigninComponent },
  {
    path: 'spents',
    component: SpentsComponent,
    canActivate: [AuthGuard],
  },
  { path: 'spentdetail/:id', component: SpentDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
